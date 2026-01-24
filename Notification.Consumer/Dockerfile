# Stage 1: Build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers for better caching
COPY *.csproj ./
RUN dotnet restore -r linux-arm64

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app/publish -r linux-arm64 --self-contained false

# Stage 2: Run
# Use the runtime image for a smaller footprint
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app

# ONLY copy the published output from the build stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Notification.Consumer.dll"]