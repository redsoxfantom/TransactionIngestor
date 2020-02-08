from dataclasses import dataclass
import re

@dataclass
class RawConverterData:
    ParsedTransaction: str
    TransactionRegex: re

class RawConverter:
    def __init__(self,dataproducer,updateneededmethod):
        self._dataproducer = dataproducer
        self._updateneededmethod = updateneededmethod

    def get_records(self):
        for record in self._dataproducer.get_records():
            if not record.ParsedTransactionType:
                pass
            yield record