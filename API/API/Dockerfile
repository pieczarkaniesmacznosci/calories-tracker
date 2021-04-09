# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /api
EXPOSE 5005

ENV ConnectionString:SqlServer "Server=tracly-db;Database=TraclyDB;Integrated Security=False;User ID=sa;Password=secretAdmin17!;"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# WORKDIR /src

# Copy csproj and restore
# COPY API.sln ./
COPY API.csproj ./src/
COPY ./../../Data/Data.csproj ./src/
# COPY Data/*.csproj ./Data/
# RUN dotnet restore
RUN dotnet restore ./src/

COPY API/API/ ./src/
COPY ./../../Data/ ./src/
WORKDIR /src/
RUN dotnet build -c Debug -o /api

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /api
FROM base AS final

WORKDIR /api
COPY --from=publish /api .
ENTRYPOINT ["dotnet", "API.dll"]

# FROM build AS publish
# RUN dotnet publish -c Debug -o /api/publish

# # Copy everything else and build
# # COPY Web/. ./Web/
# # COPY ./..Data/ ./Data/
# # COPY Web/*.json ./Web/
# # RUN dotnet publish -c Debug -o out

# # Generate runtime image
# FROM base AS final
# WORKDIR /api
# COPY --from=publish /api .
# ENTRYPOINT ["dotnet", "API.dll"]
