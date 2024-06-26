FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore
COPY . .
WORKDIR "/src/ChatConnectAPI"
RUN dotnet build "ChatConnectAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatConnectAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatConnectAPI.dll", "--urls", "http://0.0.0.0:8080"]