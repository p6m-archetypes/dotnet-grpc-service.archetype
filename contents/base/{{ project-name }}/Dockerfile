FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from your local bin folder to the Docker image
COPY {{ ProjectName }}.Server/bin/Release/net8.0/publish/ /app/

EXPOSE 8080
EXPOSE 8081

# Set the entry point to the app's executable
ENTRYPOINT ["dotnet", "{{ ProjectName }}.Server.dll"]

