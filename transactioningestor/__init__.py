import os
from pathlib import Path
from transactioningestor.input import InputManager
from transactioningestor.combiners import createcombiner

def get_transaction_dir():
    if "TRANSACTIONS_DIR" in os.environ:
        if not os.path.isdir(os.environ["TRANSACTIONS_DIR"]) and not os.path.isfile(os.environ["TRANSACTIONS_DIR"]):
            os.makedirs(os.environ["TRANSACTIONS_DIR"])
        return os.environ["TRANSACTIONS_DIR"]
    homepath = os.path.join(Path.home(),"Transactions")
    if not os.path.isdir(homepath):
        os.makedirs(homepath)
    return homepath

class TransactionIngestor:
    def __init__(self,ingestortype,inputfile,updateneededmethod,outputtype,outputfile,filetocombinewith = None,extraneoustag = None):
        self._inputmanager = InputManager(ingestortype,inputfile,updateneededmethod,extraneoustag)
        self._combiner = createcombiner(outputtype,self._inputmanager,filetocombinewith)

    def parsetransactions(self):
        pass