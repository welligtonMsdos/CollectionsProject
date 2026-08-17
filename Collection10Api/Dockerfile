FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Collection10Api/Collection10Api.csproj", "Collection10Api/"]
RUN dotnet restore "./Collection10Api/Collection10Api.csproj"
COPY . .
WORKDIR "/src/Collection10Api"
RUN dotnet build "./Collection10Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Collection10Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

ENV ASPNETCORE_URLS=http://+:8080

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Collection10Api.dll"]