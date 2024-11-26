FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cart.Microservice.API/Cart.Microservice.API.csproj", "Cart.Microservice.API/"]
COPY ["Cart.Microservice.Application/Cart.Microservice.Application.csproj", "Cart.Microservice.Application/"]
COPY ["Cart.Microservice.Domain/Cart.Microservice.Domain.csproj", "Cart.Microservice.Domain/"]
COPY ["Cart.Microservice.Infrastructure/Cart.Microservice.Infrastructure.csproj", "Cart.Microservice.Infrastructure/"]
RUN dotnet restore "./Cart.Microservice.API/Cart.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Cart.Microservice.API"
RUN dotnet build "./Cart.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cart.Microservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.Microservice.API.dll"]