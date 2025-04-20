# Stage 1: Build/Publish the API
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish Nomayini.Apis.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim-arm64v8
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./Nomayini.Apis"]
