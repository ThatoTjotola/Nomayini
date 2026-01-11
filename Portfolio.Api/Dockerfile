# Stage 1: Build/Publish the API
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
WORKDIR /src
COPY ["/","Portfolio/Portfolio.Api/"]

WORKDIR /src/Portfolio/Portfolio.Api
RUN dotnet restore "./Portfolio.Api.csproj" -a $TARGETARCH
RUN dotnet build "./Portfolio.Api.csproj" -c Release -o /app/build

RUN dotnet publish "./Portfolio.Api.csproj" -c Release -o /app/publish -a $TARGETARCH --self-contained false

FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:10.0 AS run
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]