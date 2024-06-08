FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FoodPlanner/FoodPlanner.WebApi/FoodPlanner.WebApi.csproj", "src/FoodPlanner/FoodPlanner.WebApi/"]
COPY ["src/FoodPlanner/FoodPlanner.BusinessLogic/FoodPlanner.BusinessLogic.csproj", "src/FoodPlanner/FoodPlanner.BusinessLogic/"]
RUN dotnet restore "src/FoodPlanner/FoodPlanner.WebApi/FoodPlanner.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FoodPlanner/FoodPlanner.WebApi"
RUN dotnet build "FoodPlanner.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodPlanner.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodPlanner.WebApi.dll"]