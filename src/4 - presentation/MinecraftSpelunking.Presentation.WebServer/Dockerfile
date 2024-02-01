#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/MinecraftSpelunking.Presentation.WebServer.csproj", "src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/"]
RUN dotnet restore "./src/4 - presentation/MinecraftSpelunking.Presentation.WebServer/./MinecraftSpelunking.Presentation.WebServer.csproj"
COPY . .
WORKDIR "/src/src/4 - presentation/MinecraftSpelunking.Presentation.WebServer"
RUN dotnet build "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MinecraftSpelunking.Presentation.WebServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinecraftSpelunking.Presentation.WebServer.dll"]