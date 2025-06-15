#!/bin/bash
# This script is used to set up the development environment for the project.

set -e

cleanup() {
    echo "Stopping and removing Postgres container..."

    docker-compose down --volumes --remove-orphans

    docker rm -f play-spot_postgres || true

    # Uncomment the following line if you want to remove the persistent volume
    # echo "(Optional) Removing persistent volume..."
    # docker volume rm pgdata || true

    echo
    echo "Development environment cleaned up."
    read -n 1 -s -r -p "Press any key to exit..."
    echo
}

trap cleanup EXIT

# Start Postgres via Docker
echo "Starting Postgres..."
docker run --name play-spot_postgres \
    -e POSTGRES_USER=postgres \
    -e POSTGRES_PASSWORD=password \
    -e POSTGRES_DB=play-spot_db \
    -p 5432:5432 \
    -v pgdata:/var/lib/postgresql/data \
    -v "$(pwd)"/db/init:/docker-entrypoint-initdb.d \
    -d postgres:latest

# echo "Waiting for Postgres to start..."
# sleep 5

# # Navigate to the project directory
# cd "PlaySpotApi"


host="${1:-localhost}"

set +e
echo "⏳ Waiting for database at $host..."
# until PGPASSWORD=$POSTGRES_PASSWORD psql -h "$host" -U "$POSTGRES_USER" -d "$POSTGRES_DB" -c '\q'; do
#   >&2 echo "Database is unavailable - retrying in 2s..."
#   sleep 2
# done
until docker exec -e PGPASSWORD=password play-spot_postgres psql -U postgres -d play-spot_db -c '\q' >/dev/null 2>&1; do
    >&2 echo "Database is unavailable - retrying in 2s..."
    sleep 2
set -e
echo "✔ Database is up"

# Export connection string
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Username=postgres;Password=password;Database=play-spot_db"

# Run ASP.NET Core application
echo "Starting ASP.NET Core application..."
# dotnet watch run --project PlaySpotApi.csproj
docker-compose up --build --force-recreate
