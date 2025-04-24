#!/bin/bash

echo "Stopping and removing Postgres container..."
docker rm -f play-spot_postgres

# echo "(Optional) Removing persistent volume..."
# docker volume rm myapp_pgdata