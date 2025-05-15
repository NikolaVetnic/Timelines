#!/bin/bash

set -e

CERT_DIR="./certs"
PFX_PASSWORD="YourPassword"

echo "⚠️ For use in development only - do NOT use in production scenarios!"

echo "📂 Creating certificate directory: $CERT_DIR"
mkdir -p "$CERT_DIR"

echo "🔒 Generating ASP.NET development certificate (aspnetapp.pfx)"
dotnet dev-certs https -ep "$CERT_DIR/aspnetapp.pfx" -p "$PFX_PASSWORD"

echo "🔐 Generating Nginx self-signed certificate (tl-cert.pem and tl-key.pem)"
openssl req -x509 -nodes -days 365 \
  -newkey rsa:2048 \
  -keyout "$CERT_DIR/tl-key.pem" \
  -out "$CERT_DIR/tl-cert.pem" \
  -subj "/CN=localhost"

echo "✅ Certificates generated in $CERT_DIR"