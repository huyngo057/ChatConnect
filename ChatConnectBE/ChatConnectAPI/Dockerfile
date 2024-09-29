# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development

# Build and SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files first to cache the restore step
COPY ./ChatConnectAPI/ChatConnectAPI.csproj ./ChatConnectAPI/
RUN dotnet restore "./ChatConnectAPI/ChatConnectAPI.csproj"

# Copy the entire source code and build the app
COPY . .
WORKDIR "/src/ChatConnectAPI"
RUN dotnet build "ChatConnectAPI.csproj" -c Release -o /app/build

# Publish stage for production
FROM build AS publish   
RUN dotnet publish "ChatConnectAPI.csproj" -c Release -o /app/publish

# Final image to run the app in production
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatConnectAPI.dll", "--urls", "http://0.0.0.0:8080"]
