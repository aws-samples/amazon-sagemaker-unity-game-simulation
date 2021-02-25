#!/bin/bash

echo "Starting Xvfb"
Xvfb :99 -screen 0 1024x768x24 &
sleep 2

echo "Executing command $@"
export DISPLAY=:99

exec "$@"