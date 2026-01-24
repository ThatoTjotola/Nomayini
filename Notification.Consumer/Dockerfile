# Stage 1: Build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy project from the subfolder
COPY *.csproj ./
RUN dotnet restore -r linux-arm64

COPY . ./

# Publish to /app (clean and direct)
RUN dotnet publish -c Release -o out -r linux-arm64 --self-contained false

# Stage 2: Run
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:10.0 AS run
WORKDIR /app
# Copy everything from /app in build to /app here
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Notification.Consumer.dll"]