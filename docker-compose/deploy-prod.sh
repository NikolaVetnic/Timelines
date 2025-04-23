#!/bin/bash
docker compose --env-file .env.prod -f docker-compose.yml -f docker-compose.prod.override.yml up -d --build