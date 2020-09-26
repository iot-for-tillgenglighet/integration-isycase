FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app
COPY *.sln .
COPY Integration-Isycase/*.csproj ./Integration-Isycase/
RUN dotnet restore

# copy full solution over
COPY . .
RUN dotnet build

# publish the API
FROM build AS publish
WORKDIR /app/Integration-Isycase/
RUN dotnet publish -c Release -o out

# run the api
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=publish /app/Integration-Isycase/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Integration-Isycase.dll"]


#this file is not finished.