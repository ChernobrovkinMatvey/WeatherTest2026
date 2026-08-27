FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WeatherUi/WeatherUi.csproj", "WeatherUi/"]
COPY ["Weather.Application/Weather.Application.csproj", "Weather.Application/"]
COPY ["Weather.Domain/Weather.Domain.csproj", "Weather.Domain/"]
COPY ["Weather.Infrastructure/Weather.Infrastructure.csproj", "Weather.Infrastructure/"]
RUN dotnet restore "WeatherUi/WeatherUi.csproj"
COPY . .
WORKDIR "/src/WeatherUi"
RUN dotnet build "./WeatherUi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WeatherUi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherUi.dll"]
