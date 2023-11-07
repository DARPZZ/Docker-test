#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5098

ENV DOTNET_URLS=http://+:5106

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Docker/Docker.csproj", "Docker/"]
RUN dotnet restore "Docker/Docker.csproj"
COPY . .
WORKDIR "/src/Docker"
RUN dotnet build "Docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.dll"]