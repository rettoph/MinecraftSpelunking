#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dotnet
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/MinecraftSpelunking.Presentation.WebServer.csproj", "4 - presentation/MinecraftSpelunking.Presentation.WebServer/"]
RUN dotnet restore "./4 - presentation/MinecraftSpelunking.Presentation.WebServer/./MinecraftSpelunking.Presentation.WebServer.csproj"
COPY src .
WORKDIR "/src/4 - presentation/MinecraftSpelunking.Presentation.WebServer"
RUN dotnet build "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build-dotnet AS publish-dotnet
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM node AS build-node
WORKDIR /src
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/package.json", "4 - presentation/MinecraftSpelunking.Presentation.WebServer/"]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/package-lock.json", "4 - presentation/MinecraftSpelunking.Presentation.WebServer/"]
RUN npm install --prefix "4 - presentation/MinecraftSpelunking.Presentation.WebServer"
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/webpack.config.js", "4 - presentation/MinecraftSpelunking.Presentation.WebServer/"]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/Scripts/app.js", "4 - presentation/MinecraftSpelunking.Presentation.WebServer/Scripts/"]
RUN npm --output=/app/dist --mode=production --prefix "4 - presentation/MinecraftSpelunking.Presentation.WebServer" run build

FROM base AS final
WORKDIR /app
COPY --from=publish-dotnet /app/publish .
COPY --from=build-node /app/dist /app/publish/wwwroot/dist
ENTRYPOINT ["dotnet", "MinecraftSpelunking.Presentation.WebServer.dll"]