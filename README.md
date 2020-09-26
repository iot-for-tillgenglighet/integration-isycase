# integration-isycase
An egress service that forwards alerts to external systems

# Building and tagging with Docker
docker build -f Dockerfile -t iot-for-tillgenglighet/integration-isycase:latest .

# Running locally with Docker
docker run -it -e BASE_URL='https://iotsundsvall.se' -e ALERT_URL='insert url here' -e API_KEY='insert api key here' iot-for-tillgenglighet/integration-isycase
