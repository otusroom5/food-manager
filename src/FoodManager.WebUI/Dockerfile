FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY bin/Release/net7.0/linux-x64 /app
EXPOSE 80
ENTRYPOINT ["dotnet", "FoodManager.WebUI.dll"]
