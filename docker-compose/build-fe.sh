#!/bin/bash

set -e

if [ -z "$1" ]; then
  echo "Usage: ./build-fe.sh <IP_ADDRESS>"
  exit 1
fi

IP_ADDRESS=$1

# Go into frontend directory
cd ../Frontend/react-app

echo "Installing dependencies..."
npm install

echo "Building the app..."
npm run build

echo "Removing old frontend directory on remote server..."
ssh root@$IP_ADDRESS "rm -rf /var/www/frontend/ && mkdir -p /var/www/frontend/"

echo "Copying files to remote server..."
# scp -r build/* root@$IP_ADDRESS:/var/www/frontend/
ssh root@$IP_ADDRESS "mkdir -p /Timelines/Frontend/react-app/build"
scp -r build/* root@$IP_ADDRESS:/Timelines/Frontend/react-app/build

echo "Restarting proxy..."
ssh root@$IP_ADDRESS "docker restart nginx-proxy"

echo "Deployment completed successfully."