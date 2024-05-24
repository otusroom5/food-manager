FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
COPY bin/Release/net7.0/publish /app
EXPOSE 80
ENTRYPOINT ["dotnet", "FoodStorage.WebApi.dll"]