FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 30000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RealTime.Silo/RealTime.Silo.csproj", "src/RealTime.Silo/"]
COPY ["src/RealTime.Infrastructure/RealTime.Infrastructure.csproj", "src/RealTime.Infrastructure/"]
COPY ["src/RealTime.Application/RealTime.Application.csproj", "src/RealTime.Application/"]
COPY ["src/RealTime.Domain/RealTime.Domain.csproj", "src/RealTime.Domain/"]
RUN dotnet restore "src/RealTime.Silo/RealTime.Silo.csproj"
COPY . .
WORKDIR "/src/src/RealTime.Silo"
RUN dotnet build "RealTime.Silo.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RealTime.Silo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealTime.Silo.dll"]
