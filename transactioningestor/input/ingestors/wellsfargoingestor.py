from datetime import datetime
from transactioningestor.data import DataRecord
import csv

class WellsFargoIngestor(object):
    def __init__(self,inputfilename):
        self._inputfilename = inputfilename
    
    def get_elements(self):
        with open(self._inputfilename) as f:
            reader = csv.reader(f)
            for row in reader:
                transactiondate = datetime.strptime(row[0],"%m/%d/%Y")
                transactionamount = float(row[1])
                rawtransactiontype = row[4]
                yield DataRecord(rawtransactiontype,None,transactionamount,transactiondate)