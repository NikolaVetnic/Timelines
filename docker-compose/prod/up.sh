#!/bin/bash
cd "$(dirname "$0")" || exit 1
docker compose --env-file .env -f ../docker-compose.yml -f docker-compose.override.yml up -d --build