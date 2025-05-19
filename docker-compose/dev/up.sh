#!/bin/bash

cd "$(dirname "$0")" || exit 1 # Go to the directory where the script is located

# List of known services (you can expand this list)
KNOWN_SERVICES=("tl-api" "tl-frontend" "tl-database" "proxy")

# Collect valid and invalid services
VALID_SERVICES=()
INVALID_SERVICES=()

# Process passed arguments
if [ "$#" -gt 0 ]; then
  for svc in "$@"; do
    if [[ " ${KNOWN_SERVICES[*]} " == *" $svc "* ]]; then
      VALID_SERVICES+=("$svc")
    else
      echo "Warning: Unknown service '$svc' - skipping."
      INVALID_SERVICES+=("$svc")
    fi
  done
fi

# Decide what to run
if [ "${#VALID_SERVICES[@]}" -gt 0 ]; then
  docker compose --env-file .env -f ../docker-compose.yml -f docker-compose.override.yml up -d --build "${VALID_SERVICES[@]}"
elif [ "$#" -eq 0 ]; then
  docker compose --env-file .env -f ../docker-compose.yml -f docker-compose.override.yml up -d --build
else
  echo "No valid services specified. Nothing to do."
  exit 1
fi
