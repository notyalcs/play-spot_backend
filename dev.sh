#!/bin/bash
# This script is used to set up the development environment for the project.

set -e

# Start Postgres via Docker
echo "Starting Postgres..."
docker run --name play-spot_postgres \
    -e POSTGRES_USER=postgres \
    -e POSTGRES_PASSWORD=password \
    -e POSTGRES_DB=play-spot_db \
    -p 5432:5432 \
    -v pgdata:/var/lib/postgresql/data \
    -d postgres:latest

echo "Waiting for Postgres to start..."
sleep 5

# Export connection string
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Username=postgres;Password=password;Database=play-spot_db"

# Navigate to the project directory
cd "PlaySpotApi"

# Run ASP.NET Core application
echo "Starting ASP.NET Core application..."
dotnet watch run --project PlaySpotApi.csproj
