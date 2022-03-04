FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY /app/publish/kamafi.auth.core /app/
EXPOSE 80
ENTRYPOINT ["dotnet", "kamafi.auth.core.dll"]