#!/bin/bash

set -e

if [ -z "$1" ]; then
  echo "Usage: ./build-fe.sh <IP_ADDRESS>"
  exit 1
fi

IP_ADDRESS=$1

# Go into frontend directory
cd ../Frontend/react-app

echo "Updating the API_BASE_URL constant value for deployment on remote host..."
CONSTANTS_FILE="src/data/constants.jsx"
BACKUP_FILE="${CONSTANTS_FILE}.bak"
cp "$CONSTANTS_FILE" "$BACKUP_FILE" # backup
sed -i '' 's|http://localhost/api|/api|g' "$CONSTANTS_FILE" # update url

echo "Installing dependencies..."
npm install

echo "Building the app..."
npm run build

echo "Removing old frontend directory on remote server..."
ssh root@$IP_ADDRESS "rm -rf /var/www/frontend/ && mkdir -p /var/www/frontend/"

echo "Copying files to remote server..."
scp -r build/* root@$IP_ADDRESS:/var/www/frontend/

echo "Restarting proxy..."
ssh root@$IP_ADDRESS "docker restart nginx-proxy"

echo "Revert changes made to the API_BASE_URL constant value..."
mv "$BACKUP_FILE" "$CONSTANTS_FILE"

echo "Deployment completed successfully."