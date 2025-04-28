#!/bin/bash
cd "$(dirname "$0")" || exit 1 # go to the directory where the script is located
docker compose --env-file .env -f ../docker-compose.yml -f docker-compose.override.yml down