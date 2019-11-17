#!/usr/bin/env bash

try_run() {
    echo "Running '$1'"
    eval "$1"
    if [ $? -ne 0 ]
    then
        echo "Failed to run '$1'"
        exit 1
    fi
}
