# Stage 1: Build
ARG BUILDPLATFORM
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copy from the subfolder relative to the root context
COPY ["Notification.Consumer/Notification.Consumer.csproj", "Notification.Consumer/"]

# 2. Restore using the subfolder path
RUN dotnet restore "Notification.Consumer/Notification.Consumer.csproj" -r linux-arm64

# 3. Copy the entire repository context
COPY . .

# 4. Change directory to the project folder for publishing
WORKDIR "/src/Notification.Consumer"
RUN dotnet publish "Notification.Consumer.csproj" -c Release -o /app -r linux-arm64 --self-contained false

FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:10.0 AS run
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Notification.Consumer.dll"]