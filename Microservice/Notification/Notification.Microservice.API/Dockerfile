FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Notification.Microservice.API/Notification.Microservice.API.csproj", "Notification.Microservice.API/"]
COPY ["Notification.Microservice.Application/Notification.Microservice.Application.csproj", "Notification.Microservice.Application/"]
COPY ["Notification.Microservice.Domain/Notification.Microservice.Domain.csproj", "Notification.Microservice.Domain/"]
COPY ["Notification.Microservice.Infrastructure/Notification.Microservice.Infrastructure.csproj", "Notification.Microservice.Infrastructure/"]
RUN dotnet restore "./Notification.Microservice.API/Notification.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Notification.Microservice.API"
RUN dotnet build "./Notification.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Notification.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Microservice.API.dll"]