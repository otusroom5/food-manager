#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FoodUserNotifier/FoodUserNotifier.WebAPI/FoodUserNotifier.WebApi.csproj", "src/FoodUserNotifier/FoodUserNotifier.WebAPI/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.BusinessLogic/FoodUserNotifier.BusinessLogic.csproj", "src/FoodUserNotifier/FoodUserNotifier.BusinessLogic/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.DataAccess/FoodUserNotifier.DataAccess.csproj", "src/FoodUserNotifier/FoodUserNotifier.DataAccess/"]
RUN dotnet restore "src/FoodUserNotifier/FoodUserNotifier.WebAPI/FoodUserNotifier.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FoodUserNotifier/FoodUserNotifier.WebAPI"
RUN dotnet build "FoodUserNotifier.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodUserNotifier.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodUserNotifier.WebApi.dll"]