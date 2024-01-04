# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /api

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY API/API.csproj ./src/API/
COPY Data/Data.csproj ./src/Data/
WORKDIR /src/API/
RUN dotnet restore

COPY ./API/ .
WORKDIR /src/Data/
COPY ./Data/ .
WORKDIR /src/API/

FROM build AS publish
RUN dotnet publish "API.csproj" -c Debug -o /api
FROM base AS final

WORKDIR /api
COPY --from=publish /api .
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "API.dll"]