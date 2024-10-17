# Base stage for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7192
ENV ASPNETCORE_ENVIRONMENT=Development

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./ChatHub/ChatHub.csproj ./ChatHub/
RUN dotnet restore "./ChatHub/ChatHub.csproj" --no-cache

# Publish stage for production
FROM build AS publish
COPY . .
WORKDIR /src/ChatHub
RUN dotnet publish "ChatHub.csproj" -c Release -o /app/publish

# Dev stage (for development with dotnet watch)
FROM build AS dev
WORKDIR /src/ChatHub
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:7192"]

# Final stage (for production)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatHub.dll", "--urls", "http://0.0.0.0:7192"]