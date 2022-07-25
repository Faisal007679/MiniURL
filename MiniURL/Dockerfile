#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
ENV ASPNETCORE_URLS="http://+:54000"

WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src

FROM build AS publish
COPY . .

FROM base AS final
WORKDIR /app
COPY . .

# Entrypoint and network configuration
EXPOSE 80
ENTRYPOINT ["dotnet", "MiniURL.dll"]