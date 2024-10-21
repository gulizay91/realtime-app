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
docker build -f src/RealTime.API/Dockerfile -t realtime-app .
docker run --rm --name realtime-app -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development -d realtime-app
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