.PHONY: run_docker stop_docker docker_latest_image

run_docker:
	docker rm -f realtime-app || true && \
       docker rmi -f \$(docker images -q realtime-app) || true && \
    		docker build -f src/RealTime.API/Dockerfile -t realtime-app . && \
    			docker run --rm --name realtime-app -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development -d realtime-app

stop_docker:
	docker stop realtime-app

docker_latest_image:
	docker build -f src/RealTime.API/Dockerfile -t realtime-app . && \
		docker tag realtime-app guliz91/realtime-app:latest && \
			docker push guliz91/realtime-app:latest

