FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CachingInDotNet7/CachingInDotNet7.csproj", "CachingInDotNet7/"]
RUN dotnet restore "CachingInDotNet7/CachingInDotNet7.csproj"
COPY . .
WORKDIR "/src/CachingInDotNet7"
RUN dotnet build "CachingInDotNet7.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CachingInDotNet7.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CachingInDotNet7.dll"]
