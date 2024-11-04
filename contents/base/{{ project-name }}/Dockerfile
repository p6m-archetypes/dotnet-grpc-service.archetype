FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

WORKDIR /app
COPY --from=build /app/publish .
# Set the entry point to the app's executable
ENTRYPOINT ["dotnet", "{{ ProjectName }}.Server.dll"]

