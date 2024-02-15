#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM node as node-base

FROM node-base AS node-install
WORKDIR /src
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/package.json", "."]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/package-lock.json", "."]
RUN npm install

FROM node-install AS node-build
WORKDIR /src
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/Scripts", "./Scripts"]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/Styles", "./Styles"]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/rollup.*", "./"]
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/tsconfig.*", "./"]
RUN npm run dev
RUN mkdir /dist
RUN mv ./wwwroot/dist/* /dist

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
ARG BUILD_CONFIGURATION=Release
WORKDIR /libraries
COPY ["libraries", "."]
WORKDIR /src
COPY ["src", "."]
RUN dotnet restore "./4 - presentation/MinecraftSpelunking.Presentation.WebServer/./MinecraftSpelunking.Presentation.WebServer.csproj"
WORKDIR "/src/4 - presentation/MinecraftSpelunking.Presentation.WebServer"
COPY --from=node-build /dist ./wwwroot/dist

# Build application
RUN dotnet build "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM dotnet-build AS dotnet-publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false



FROM base AS final
WORKDIR /app
COPY --from=dotnet-publish /app/publish .
ENTRYPOINT ["dotnet", "MinecraftSpelunking.Presentation.WebServer.dll"]