FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RealTime.API/RealTime.API.csproj", "src/RealTime.API/"]
COPY ["src/RealTime.Application/RealTime.Application.csproj", "src/RealTime.Application/"]
COPY ["src/RealTime.Domain/RealTime.Domain.csproj", "src/RealTime.Domain/"]
COPY ["src/RealTime.Infrastructure/RealTime.Infrastructure.csproj", "src/RealTime.Infrastructure/"]
RUN dotnet restore "src/RealTime.API/RealTime.API.csproj"
COPY . .
WORKDIR "/src/src/RealTime.API"
RUN dotnet build "RealTime.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RealTime.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealTime.API.dll"]
