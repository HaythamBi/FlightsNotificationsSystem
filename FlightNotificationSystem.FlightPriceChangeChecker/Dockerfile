#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlightNotificationSystem.FlightPriceChangeChecker/FlightNotificationSystem.FlightPriceChangeChecker.csproj", "FlightNotificationSystem.FlightPriceChangeChecker/"]
COPY ["FlightNotificationSystem.Data/FlightNotificationSystem.Data.csproj", "FlightNotificationSystem.Data/"]
COPY ["FlightNotificationSystem.Notification/FlightNotificationSystem.Notification.csproj", "FlightNotificationSystem.Notification/"]
COPY ["FlightNotificationSystem.Shared/FlightNotificationSystem.Shared.csproj", "FlightNotificationSystem.Shared/"]
RUN dotnet restore "./FlightNotificationSystem.FlightPriceChangeChecker/FlightNotificationSystem.FlightPriceChangeChecker.csproj"
COPY . .
WORKDIR "/src/FlightNotificationSystem.FlightPriceChangeChecker"
RUN dotnet build "./FlightNotificationSystem.FlightPriceChangeChecker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlightNotificationSystem.FlightPriceChangeChecker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightNotificationSystem.FlightPriceChangeChecker.dll"]