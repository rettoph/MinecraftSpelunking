#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM node as node-base

FROM node-base AS node-install
WORKDIR /web-app
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/package.json", "."]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/package-lock.json", "."]
RUN npm install

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dotnet
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src", "."]
RUN dotnet restore "./4 - presentation/MinecraftSpelunking.Presentation.WebServer/./MinecraftSpelunking.Presentation.WebServer.csproj"
WORKDIR "/src/4 - presentation/MinecraftSpelunking.Presentation.WebServer"

# Copy node, npm, & node_modules
COPY --from=node-install /usr/local /usr/local
COPY --from=node-install /web-app .

# Build application
RUN dotnet build "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build-dotnet AS publish-dotnet
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false



FROM base AS final
WORKDIR /app
COPY --from=publish-dotnet /app/publish .
ENTRYPOINT ["dotnet", "MinecraftSpelunking.Presentation.WebServer.dll"]