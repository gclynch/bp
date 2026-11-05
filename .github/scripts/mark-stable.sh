#!/usr/bin/env bash
# Usage: mark-stable.sh <image-name> <version>
#
# Run after <version> passed the live health check. Moves the "stable" tag to
# <version>, and first keeps the version it replaces as "previous", so a manual
# rollback can go back to it (skipped when re-deploying the stable version).
set -euo pipefail

image="$1"
version="$2"

if docker pull -q "$image:stable"; then
  current=$(docker image inspect "$image:stable" \
    --format '{{range .Config.Env}}{{println .}}{{end}}' | sed -n 's/^APP_VERSION=//p')
  if [ -n "$current" ] && [ "$current" != "$version" ]; then
    docker buildx imagetools create -t "$image:previous" "$image:$current"
    echo "Previous version is now $current"
  fi
else
  echo "No stable image yet, nothing to keep"
fi

docker buildx imagetools create -t "$image:stable" "$image:$version"
