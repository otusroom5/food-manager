#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FoodUserNotifier/FoodUserNotifier.Application.WebAPI/FoodUserNotifier.Application.WebAPI.csproj", "src/FoodUserNotifier/FoodUserNotifier.Application.WebAPI/"]
COPY ["src/Shared/FoodManager.Shared/FoodManager.Shared.csproj", "src/Shared/FoodManager.Shared/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Repositories/FoodUserNotifier.Infrastructure.Repositories.csproj", "src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Repositories/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Core.Entities/FoodUserNotifier.Core.Entities.csproj", "src/FoodUserNotifier/FoodUserNotifier.Core.Entities/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Core.Interfaces/FoodUserNotifier.Core.Interfaces.csproj", "src/FoodUserNotifier/FoodUserNotifier.Core.Interfaces/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Services/FoodUserNotifier.Infrastructure.Services.csproj", "src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Services/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Core.Domain/FoodUserNotifier.Core.Domain.csproj", "src/FoodUserNotifier/FoodUserNotifier.Core.Domain/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Smtp/FoodUserNotifier.Infrastructure.Smtp.csproj", "src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Smtp/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Sources/FoodUserNotifier.Infrastructure.Sources.csproj", "src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Sources/"]
COPY ["src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Telegram/FoodUserNotifier.Infrastructure.Telegram.csproj", "src/FoodUserNotifier/FoodUserNotifier.Infrastructure.Telegram/"]
RUN dotnet restore "src/FoodUserNotifier/FoodUserNotifier.Application.WebAPI/FoodUserNotifier.Application.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/FoodUserNotifier/FoodUserNotifier.Application.WebAPI"
RUN dotnet build "FoodUserNotifier.Application.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodUserNotifier.Application.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodUserNotifier.Application.WebAPI.dll"]