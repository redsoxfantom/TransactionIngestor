from transactioningestor.data import DataRecord
from datetime import datetime
import ijson

class StandardFormatJsonIngestor(object):
    
    def __init__(self,inputfilename):
        self._inputfilename = inputfilename

    def get_elements(self):
        with open(self._inputfilename, 'br') as f:
            for prefix, event, value in ijson.parse(f):
                if prefix == 'item.TransactionDate':
                    self.transactiondate = datetime.strptime(value,"%Y-%m-%dT%H:%M:%S")
                if prefix == 'item.TransactionAmount':
                    self.transactionamount = value
                if prefix == 'item.RawTransactionType':
                    self.rawtransactiontype = value
                if prefix == 'item.ParsedTransactionType':
                    yield DataRecord(self.rawtransactiontype,value,self.transactionamount, self.transactiondate)