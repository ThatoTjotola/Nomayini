# Stage 1: Build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project from the subfolder
COPY ["Notification.Consumer/Notification.Consumer.csproj", "Notification.Consumer/"]
RUN dotnet restore "Notification.Consumer/Notification.Consumer.csproj" -r linux-arm64

COPY . .

WORKDIR "/src/Notification.Consumer"
# Publish to /app (clean and direct)
RUN dotnet publish "Notification.Consumer.csproj" -c Release -o /app -r linux-arm64 --self-contained false

# Stage 2: Run
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:10.0 AS run
WORKDIR /app
# Copy everything from /app in build to /app here
COPY --from=build /app .

# Port 80 is standard for containers
ENTRYPOINT ["dotnet", "Notification.Consumer.dll"]