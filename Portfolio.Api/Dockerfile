
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src


COPY ["Portfolio.Api/Portfolio.Api.csproj", "Portfolio.Api/"]

RUN dotnet restore "Portfolio.Api/Portfolio.Api.csproj" -a linux/arm64

COPY . .

WORKDIR "/src/Portfolio.Api"
RUN dotnet publish "Portfolio.Api.csproj" \
    -c Release \
    -o /app/publish \
    -a linux/arm64 \
    --self-contained false \
    /p:UseAppHost=false

FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:10.0 AS run
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]