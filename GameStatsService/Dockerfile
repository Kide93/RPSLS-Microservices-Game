FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["GameStatsService/GameStatsService.Presentation/GameStatsService.Presentation.csproj", "GameStatsService/GameStatsService.Presentation/"]
COPY ["GameStatsService/GameStatsService.Business/GameStatsService.Business.csproj", "GameStatsService/GameStatsService.Business/"]
COPY ["GameStatsService/GameStatsService.Infrastructure/GameStatsService.Infrastructure.csproj", "GameStatsService/GameStatsService.Infrastructure/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "GameStatsService/GameStatsService.Presentation/GameStatsService.Presentation.csproj"
COPY . .
WORKDIR "/src/GameStatsService/GameStatsService.Presentation"

RUN dotnet build "./GameStatsService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GameStatsService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameStatsService.Presentation.dll"]