import sys
from pathlib import Path
import shutil
import os
from os import path 
import subprocess

def _run_command(cmd):
    print(F"Executing command: {cmd}")
    subprocess.run(cmd,shell=True,check=True)

def dotnet_installed():
    if not shutil.which('dotnet'):
        print("dotnet not installed or not on PATH")
        return False
    curr_sdk = os.popen('dotnet --version').read().strip()
    curr_sdk_split = curr_sdk.split('.')
    print(F"Detected dotnet sdk version {curr_sdk}")
    if int(curr_sdk_split[0]) < 3:
        print("dotnet sdk version needs to be >= 3")
        return False
    return True

def set_up_directories(transactionsDirectory):
    try:
        if not Path(transactionsDirectory).exists():
            print(F"Creating {transactionsDirectory}...")
            os.makedirs(transactionsDirectory)
        
        installDir = Path(path.join(transactionsDirectory,"TransactionIngestor"))
        if installDir.exists():
            print(F"Deleting old installation directory ({installDir})")
            shutil.rmtree(installDir)

        compileDir = Path(path.join(os.path.dirname(__file__),'..','output'))
        if compileDir.exists():
            print(F"Deleting old compilation outputs ({compileDir})")
            shutil.rmtree(compileDir)

    except Exception as ex:
        print(F"Failed to set up directories: {ex}")
        return False
    return True

def restore_and_compile():
    try:
        sln = os.path.join(os.path.dirname(__file__),"..","TransactionIngestor.sln")
        _run_command(F"dotnet restore {sln}")
        compileDir = path.join(os.path.dirname(__file__),'..','output')
        _run_command(F"dotnet publish -o {compileDir} {sln}")
    except Exception as ex:
        print(F"Failed to restore and compile: {ex}")
        return False
    return True

def install(args):
    print("Checking required software")
    if len(args) == 2:
        transactionsDirectory = args[1]
    else:
        transactionsDirectory = os.path.join(str(Path.home()),"Transactions")
    if not dotnet_installed():
        return False
    if not set_up_directories(transactionsDirectory):
        return False
    if not restore_and_compile():
        return False
    return True

if __name__ == "__main__":
    if not install(sys.argv):
        print("Install Failed")
        sys.exit(1)
    else:
        print("Install Succeded")