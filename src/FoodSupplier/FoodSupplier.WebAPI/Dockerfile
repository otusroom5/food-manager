FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY bin/Release/net7.0 /app
EXPOSE 80
ENTRYPOINT ["dotnet", "FoodSupplier.WebApi.dll"]