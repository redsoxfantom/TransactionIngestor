from dataclasses import dataclass
from datetime import datetime
import ijson

@dataclass
class DataRecord:
    RawTransactionType: str
    ParsedTransactionType: str
    TransactionAmount: float
    TransactionDate: datetime

def get_datarecords_from_file(filename):
    with open(filename, 'br') as f:
        for prefix, event, value in ijson.parse(f):
            if prefix == 'item.TransactionDate':
                transactiondate = datetime.strptime(value,"%Y-%m-%dT%H:%M:%S")
            if prefix == 'item.TransactionAmount':
                transactionamount = value
            if prefix == 'item.RawTransactionType':
                rawtransactiontype = value
            if prefix == 'item.ParsedTransactionType':
                yield DataRecord(rawtransactiontype,value,transactionamount,transactiondate)