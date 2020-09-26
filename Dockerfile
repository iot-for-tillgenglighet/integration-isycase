FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY *.sln .
COPY Integration-Isycase/*.csproj ./Integration-Isycase/
RUN dotnet restore

COPY . .
RUN dotnet build
FROM build AS publish
WORKDIR /app/Integration-Isycase
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /app/Integration-Isycase/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Integration-Isycase.dll"]