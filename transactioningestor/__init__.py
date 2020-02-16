import os
from pathlib import Path

def get_transaction_dir():
    if "TRANSACTIONS_DIR" in os.environ:
        if not os.path.isdir(os.environ["TRANSACTIONS_DIR"]) and not os.path.isfile(os.environ["TRANSACTIONS_DIR"]):
            os.makedirs(os.environ["TRANSACTIONS_DIR"])
        return os.environ["TRANSACTIONS_DIR"]
    homepath = os.path.join(Path.home(),"Transactions")
    if not os.path.isdir(homepath):
        os.makedirs(homepath)
    return homepath