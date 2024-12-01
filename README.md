# realtime-app

## Tech

![Clean Architecture](etc/clean-architecture.png?raw=true)
![Clean Architecture DDD](etc/clean-domain-driven-design-jacobs.png?raw=true)
### Framework And Libraries
* Framework: .NET 8
* CQRS: MediatR
* RealTime: SignalR
* State Management: Orleans
* Architecture: Clean Architecture
* ExternalServices: Twilio API
* Tests: xUnit
* FastEndpoints, FluentValidation, Swashbuckle, Mapster


## Docker
### build docker image
```sh
# /realtime-app>
docker build -f src/RealTime.Silo/Dockerfile -t realtime-silo .
docker run --rm --name realtime-silo -p 30000:30000 -p 8080:8080 -d realtime-silo

docker build -f src/RealTime.API/Dockerfile -t realtime-api .
docker run --rm --name realtime-api -p 5031:5031 -e ASPNETCORE_ENVIRONMENT=Development -e ORLEANS_SILO_ADDRESS=realtime-silo-host -d realtime-api

docker-compose -p realtime-app up
docker-compose -p realtime-app down --volumes --remove-orphans
```
or you can use phony target
```sh
# /realtime-app>
make run_docker
make stop_docker
make docker_latest_image
```

### push image to docker hub
```sh
docker login --username=guliz91 # you will prompted for your password
docker tag realtime-app guliz91/realtime-app:latest # tag docker image
docker push guliz91/realtime-app:latest # push docker image to docker hub
```