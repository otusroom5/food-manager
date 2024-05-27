FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FoodPlanner/FoodPlanner.WebApi/FoodPlanner.WebApi.csproj", "src/FoodPlanner/FoodPlanner.WebApi/"]
RUN dotnet restore "./src/FoodPlanner/FoodPlanner.WebApi/FoodPlanner.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FoodPlanner/FoodPlanner.WebApi"
RUN dotnet build "./FoodPlanner.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FoodPlanner.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodPlanner.WebApi.dll"]