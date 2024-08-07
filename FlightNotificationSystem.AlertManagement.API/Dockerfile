#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlightNotificationSystem.AlertManagement.API/FlightNotificationSystem.AlertManagement.API.csproj", "FlightNotificationSystem.AlertManagement.API/"]
COPY ["FlightNotificationSystem.Data/FlightNotificationSystem.Data.csproj", "FlightNotificationSystem.Data/"]
RUN dotnet restore "./FlightNotificationSystem.AlertManagement.API/FlightNotificationSystem.AlertManagement.API.csproj"
COPY . .
WORKDIR "/src/FlightNotificationSystem.AlertManagement.API"
RUN dotnet build "./FlightNotificationSystem.AlertManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlightNotificationSystem.AlertManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightNotificationSystem.AlertManagement.API.dll"]