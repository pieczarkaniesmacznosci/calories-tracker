# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /api

# Copy csproj and restore
COPY *.sln .
COPY Web/*.csproj ./Web/
COPY /Data/*.csproj ./../Data/
RUN dotnet restore

# Copy everything else and build
COPY Web/. ./Web/
COPY ./..Data/ ./Data/
# COPY Web/*.json ./Web/
RUN dotnet publish -c Debug -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /api
EXPOSE 80
COPY --from=build-env /api/out .
ENTRYPOINT ["dotnet", "Web.dll"]