#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FoodSupplier/FoodSupplier.WebAPI/FoodSupplier.WebAPI.csproj", "src/FoodSupplier/FoodSupplier.WebAPI/"]
COPY ["src/FoodSupplier/FoodSupplier.BusinessLogic/FoodSupplier.BusinessLogic.csproj", "src/FoodSupplier/FoodSupplier.BusinessLogic/"]
COPY ["src/FoodSupplier/FoodSupplier.DataAccess/FoodSupplier.DataAccess.csproj", "src/FoodSupplier/FoodSupplier.DataAccess/"]
RUN dotnet restore "src/FoodSupplier/FoodSupplier.WebAPI/FoodSupplier.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/FoodSupplier/FoodSupplier.WebAPI"
RUN dotnet build "FoodSupplier.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodSupplier.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodSupplier.WebAPI.dll"]