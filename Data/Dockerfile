
# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /data
EXPOSE 80
ENV ConnectionString:SqlServer "Server=tracly-db;Database=TraclyDB;Integrated Security=False;User ID=sa;Password=secretAdmin17!;"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY Data.csproj ./src/
RUN dotnet restore ./src/

COPY . ./src/
WORKDIR /src/
RUN dotnet build -c Debug -o /data

FROM build AS publish
RUN dotnet publish "Data.csproj" -c Release -o /data
FROM base AS final

WORKDIR /data
COPY --from=publish /data .
ENTRYPOINT ["dotnet", "Data.dll"]