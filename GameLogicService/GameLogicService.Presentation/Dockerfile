FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["GameLogicService/GameLogicService.Presentation/GameLogicService.Presentation.csproj", "GameLogicService/GameLogicService.Presentation/"]
COPY ["GameLogicService/GameLogicService.Business/GameLogicService.Business.csproj", "GameLogicService/GameLogicService.Business/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "GameLogicService/GameLogicService.Presentation/GameLogicService.Presentation.csproj"

COPY . .
WORKDIR "/src/GameLogicService/GameLogicService.Presentation"
RUN dotnet build "./GameLogicService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GameLogicService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameLogicService.Presentation.dll"]