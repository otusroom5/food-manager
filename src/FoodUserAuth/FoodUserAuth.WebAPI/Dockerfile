#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FoodUserAuth/FoodUserAuth.WebAPI/FoodUserAuth.WebApi.csproj", "src/FoodUserAuth/FoodUserAuth.WebAPI/"]
COPY ["src/Shared/FoodManager.Shared/FoodManager.Shared.csproj", "src/Shared/FoodManager.Shared/"]
COPY ["src/FoodUserAuth/FoodUserAuth.BusinessLogic/FoodUserAuth.BusinessLogic.csproj", "src/FoodUserAuth/FoodUserAuth.BusinessLogic/"]
COPY ["src/FoodUserAuth/FoodUserAuth.DataAccess/FoodUserAuth.DataAccess.csproj", "src/FoodUserAuth/FoodUserAuth.DataAccess/"]
RUN dotnet restore "src/FoodUserAuth/FoodUserAuth.WebAPI/FoodUserAuth.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FoodUserAuth/FoodUserAuth.WebAPI"
RUN dotnet build "FoodUserAuth.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodUserAuth.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodUserAuth.WebApi.dll"]