#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FoodStorage/FoodStorage.WebAPI/FoodStorage.WebApi.csproj", "src/FoodStorage/FoodStorage.WebAPI/"]
COPY ["src/FoodStorage/Core.Application/Application.Implementations/FoodStorage.Application.Implementations.csproj", "src/FoodStorage/Core.Application/Application.Implementations/"]
COPY ["src/FoodStorage/Core.Application/Abstractions/Application.Repositories/FoodStorage.Application.Repositories.csproj", "src/FoodStorage/Core.Application/Abstractions/Application.Repositories/"]
COPY ["src/FoodStorage/Core.Domain/Domain.Entities/FoodStorage.Domain.Entities.csproj", "src/FoodStorage/Core.Domain/Domain.Entities/"]
COPY ["src/FoodStorage/Core.Application/Abstractions/Application.Services/FoodStorage.Application.Services.csproj", "src/FoodStorage/Core.Application/Abstractions/Application.Services/"]
COPY ["src/FoodStorage/Infrastructure/Infrastructure.Implementations/FoodStorage.Infrastructure.Implementations.csproj", "src/FoodStorage/Infrastructure/Infrastructure.Implementations/"]
COPY ["src/FoodStorage/Infrastructure/Infrastructure.EntityFramework/FoodStorage.Infrastructure.EntityFramework.csproj", "src/FoodStorage/Infrastructure/Infrastructure.EntityFramework/"]
RUN dotnet restore "src/FoodStorage/FoodStorage.WebAPI/FoodStorage.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FoodStorage/FoodStorage.WebAPI"
RUN dotnet build "FoodStorage.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodStorage.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodStorage.WebApi.dll"]