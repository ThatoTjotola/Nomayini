# Stage 1: Build/Publish the API
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish Nomayini.Apis.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim-arm64v8
WORKDIR /app

# Install the .NET runtime (to avoid missing framework issues)
RUN apt-get update && apt-get install -y dotnet-runtime-9.0

# Copy the build output from the previous stage
COPY --from=build /app/publish .

# Set the entrypoint to start the app
ENTRYPOINT ["./Nomayini.Apis"]
