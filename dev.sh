#!/usr/bin/env bash
set -e

cleanup() {
    echo
    echo "🛑 Stopping and removing all containers..."
    docker-compose down --volumes --remove-orphans
    echo "✔ Development environment cleaned up."
    read -n 1 -s -r -p "Press any key to exit..."
    echo
}

trap cleanup EXIT

echo "🚀 Starting Postgres service..."
docker-compose up -d db

sleep 6

echo "🚀 Starting all remaining services..."
docker-compose up -d

echo "✔ All services are up and running. Ctrl+C to stop the services."
docker-compose logs -f
