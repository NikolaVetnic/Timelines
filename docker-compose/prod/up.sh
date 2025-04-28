#!/bin/bash

set -e

echo "Building frontend..."
cd Frontend/react-app

npm install
npm run build

cd ../../docker-compose/prod

echo "Starting docker compose..."
docker compose --env-file .env -f ../docker-compose.yml -f docker-compose.override.yml up -d --build

echo "Deployment completed."