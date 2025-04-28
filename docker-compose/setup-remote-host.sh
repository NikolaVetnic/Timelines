#!/bin/bash

set -e

if [ -z "$1" ]; then
  echo "Usage: $0 <IP-ADDRESS>"
  exit 1
fi

IP_ADDRESS=$1

echo "Connecting to $IP_ADDRESS and installing Docker and Docker Compose..."

ssh root@$IP_ADDRESS bash <<'EOF'
set -e

# --- Step 1: Install Docker ---
echo "Updating apt packages..."
apt update

echo "Installing dependencies..."
apt install -y apt-transport-https ca-certificates curl software-properties-common gnupg

echo "Adding Docker's official GPG key..."
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | gpg --dearmor -o /etc/apt/trusted.gpg.d/docker.gpg

echo "Adding Docker's APT repository..."
add-apt-repository \
  "deb [arch=$(dpkg --print-architecture)] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable"

echo "Updating apt packages after adding Docker repo..."
apt update

echo "Installing Docker..."
apt install -y docker-ce docker-ce-cli containerd.io

echo "Enabling and starting Docker service..."
systemctl enable docker
systemctl start docker

echo "Checking Docker version:"
docker --version

# --- Step 2: Install Docker Compose Plugin ---
echo "Creating directory for Docker CLI plugins..."
mkdir -p /usr/local/lib/docker/cli-plugins

echo "Downloading Docker Compose plugin..."
curl -SL https://github.com/docker/compose/releases/latest/download/docker-compose-linux-$(uname -m) -o /usr/local/lib/docker/cli-plugins/docker-compose

echo "Making Docker Compose plugin executable..."
chmod +x /usr/local/lib/docker/cli-plugins/docker-compose

echo "Checking Docker Compose version:"
docker compose version

# --- Step 3: (Optional) Add user to docker group ---
echo "Adding root user to docker group..."
usermod -aG docker root

echo ""
echo "You have been added to the docker group."
echo "Please log out and log back in, or run 'newgrp docker' to apply the new group membership immediately."
echo ""
echo "Docker and Docker Compose installation completed successfully."
EOF