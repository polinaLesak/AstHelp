FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Identity.Microservice.API/Identity.Microservice.API.csproj", "Identity.Microservice.API/"]
COPY ["Identity.Microservice.Application/Identity.Microservice.Application.csproj", "Identity.Microservice.Application/"]
COPY ["Identity.Microservice.Domain/Identity.Microservice.Domain.csproj", "Identity.Microservice.Domain/"]
COPY ["Identity.Microservice.Infrastructure/Identity.Microservice.Infrastructure.csproj", "Identity.Microservice.Infrastructure/"]
RUN dotnet restore "./Identity.Microservice.API/Identity.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Identity.Microservice.API"
RUN dotnet build "./Identity.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Microservice.API.dll"]