#!/bin/bash
docker compose --env-file .env.local -f docker-compose.yml -f docker-compose.local.override.yml up -d --build