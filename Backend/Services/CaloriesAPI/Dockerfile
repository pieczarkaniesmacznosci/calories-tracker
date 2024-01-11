#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/CaloriesAPI/CaloriesAPI.csproj", "Services/CaloriesAPI/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "./Services/CaloriesAPI/./CaloriesAPI.csproj"
COPY . .
WORKDIR "/src/Services/CaloriesAPI"
RUN dotnet build "./CaloriesAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CaloriesAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CaloriesAPI.dll"]