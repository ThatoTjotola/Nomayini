# Stage 1: Build
ARG BUILDPLATFORM
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Be explicit with the filename to avoid wildcard issues
COPY Notification.Consumer.csproj ./

# 2. (Optional Debug) List files to verify the csproj is actually there
RUN ls -la

# 3. Restore for the specific architecture
RUN dotnet restore "Notification.Consumer.csproj" -r linux-arm64

# 4. Copy everything else
COPY . .

# 5. Build and Publish
RUN dotnet publish "Notification.Consumer.csproj" -c Release -o /app/publish -r linux-arm64 --self-contained false

# Stage 2: Run
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Notification.Consumer.dll"]