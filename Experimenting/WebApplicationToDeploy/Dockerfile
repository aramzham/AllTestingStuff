# get base sdk from microsoft
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# copy the csproj file and restore any dependencies (via nuget)
COPY *.csproj ./
RUN dotnet restore

# copy the project files and build our release
COPY . ./
RUN dotnet publish -c Release -o out

# generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebApplicationToDeploy.dll"]