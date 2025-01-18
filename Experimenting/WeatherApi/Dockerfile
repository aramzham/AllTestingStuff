FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
USER $APP_UID
WORKDIR /app
EXPOSE 2024
EXPOSE 2025

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WeatherApi.dll"]
