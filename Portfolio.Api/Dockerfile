# Stage 1: Build/Publish the API
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Portfolio/Portfolio.Api/","Portfolio/Portfolio.Api/"]

WORKDIR /src/Portfolio/Portfolio.Api
RUN dotnet restore "./Portfolio.Api.csproj"
RUN dotnet build "./Portfolio.Api.csproj" -c Release -o /app/build

RUN dotnet publish "./Portfolio.Api.csproj" -c Release -o /app/publish -r linux-arm64

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS run
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]