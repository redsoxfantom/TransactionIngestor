from dataclasses import dataclass
from dataclasses_json import dataclass_json
import re
import os
import json

@dataclass
@dataclass_json
class RawConverterData:
    ParsedTransaction: str
    TransactionRegex: str

class RawConverter:
    def __init__(self,dataproducer,updateneededmethod,regexfile):
        self._dataproducer = dataproducer
        self._updateneededmethod = updateneededmethod
        self._regexfile = regexfile
        if not os.path.isfile(regexfile):
            self._loadedregexdata = []
        else:
            with open(regexfile) as f:
                self._loadedregexdata = json.load(f)
                for idx in range(0,len(self._loadedregexdata)):
                    regex = self._loadedregexdata[idx]['TransactionRegex']
                    self._loadedregexdata[idx]['TransactionRegex'] = re.compile(regex)

    def get_records(self):
        for record in self._dataproducer.get_records():
            if not record.ParsedTransactionType:
                conversionFound = False
                for loadedconversion in self._loadedregexdata:
                    if loadedconversion['TransactionRegex'].match(record.RawTransactionType):
                        record.ParsedTransactionType = loadedconversion['ParsedTransaction']
                        conversionFound = True
                        break
                if not conversionFound:
                    result = self._updateneededmethod(record)
                    record.ParsedTransactionType = result[1]
                    converterdata = RawConverterData()
                    converterdata.ParsedTransaction = result[1]
                    converterdata.TransactionRegex = result[0]
                    self._loadedregexdata.append(converterdata)
                    with open(self._regexfile, "w") as f:
                        json.dump(self._loadedregexdata,f)
            yield record