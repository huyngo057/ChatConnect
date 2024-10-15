# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development

# Build and SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./ChatConnectAPI ./ChatConnectAPI
COPY ./ChatConnectData ./ChatConnectData
RUN dotnet restore "./ChatConnectAPI/ChatConnectAPI.csproj" --no-cache

# Publish stage for production
FROM build AS publish 
COPY . .
WORKDIR /src/ChatConnectAPI
RUN dotnet publish "ChatConnectAPI.csproj" -c Release -o /app/publish

FROM build AS dev
WORKDIR /src/ChatConnectAPI
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"]

# Final image to run the app in production
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatConnectAPI.dll", "--urls", "http://0.0.0.0:8080"]
