FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN dotnet tool install --global dotnet-ef --version 6.0.1
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ./kamafi.auth.data.migrations/ ./
CMD ["dotnet", "ef", "database", "update"]