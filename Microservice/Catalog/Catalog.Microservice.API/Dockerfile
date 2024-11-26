FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Catalog.Microservice.API/Catalog.Microservice.API.csproj", "Catalog.Microservice.API/"]
COPY ["Catalog.Microservice.Application/Catalog.Microservice.Application.csproj", "Catalog.Microservice.Application/"]
COPY ["Catalog.Microservice.Domain/Catalog.Microservice.Domain.csproj", "Catalog.Microservice.Domain/"]
COPY ["Catalog.Microservice.Infrastructure/Catalog.Microservice.Infrastructure.csproj", "Catalog.Microservice.Infrastructure/"]
RUN dotnet restore "./Catalog.Microservice.API/Catalog.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Catalog.Microservice.API"
RUN dotnet build "./Catalog.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Microservice.API.dll"]