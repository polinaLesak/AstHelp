FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Orders.Microservice.API/Orders.Microservice.API.csproj", "Orders.Microservice.API/"]
COPY ["Orders.Microservice.Application/Orders.Microservice.Application.csproj", "Orders.Microservice.Application/"]
COPY ["Orders.Microservice.Domain/Orders.Microservice.Domain.csproj", "Orders.Microservice.Domain/"]
COPY ["Orders.Microservice.Infrastructure/Orders.Microservice.Infrastructure.csproj", "Orders.Microservice.Infrastructure/"]
RUN dotnet restore "./Orders.Microservice.API/Orders.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Orders.Microservice.API"
RUN dotnet build "./Orders.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Orders.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.Microservice.API.dll"]