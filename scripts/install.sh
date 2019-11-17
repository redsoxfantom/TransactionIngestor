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

if [ -z "$1" ]
then
    TransactionsDirectory=$HOME/Transactions
else
    TransactionsDirectory=$1
fi
try_run "mkdir -p $TransactionsDirectory"

InstallDir=$TransactionsDirectory/TransactionIngestor
if [ -d $InstallDir ]
then
    try_run "rm -rf $InstallDir"
fi