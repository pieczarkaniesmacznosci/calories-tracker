# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /api
ENV ConnectionString:SqlServer "Server=tracly-db;Database=TraclyDB;Integrated Security=False;User ID=ENVID;Password=ENVDBPW;"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY API/API/API.csproj ./src/API/API/
COPY Data/Data.csproj ./src/Data/
WORKDIR /src/API/API/
RUN dotnet restore

COPY ./API/API/ .
WORKDIR /src/Data/
COPY ./Data/ .
WORKDIR /src/API/API/

FROM build AS publish
RUN dotnet publish "API.csproj" -c Debug -o /api
FROM base AS final

WORKDIR /api
COPY --from=publish /api .
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "API.dll"]