FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7192

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChatHub/ChatHub.csproj", "ChatHub/"]
RUN dotnet restore "ChatHub/ChatHub.csproj"
COPY . . 
WORKDIR "/src/ChatHub"
RUN dotnet build "ChatHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatHub.dll", "--urls", "http://0.0.0.0:7192"]