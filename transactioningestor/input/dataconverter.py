import re
import os
import json

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

    def get_records(self):
        for record in self._dataproducer.get_records():
            if not record.ParsedTransactionType:
                conversionFound = False
                for loadedconversion in self._loadedregexdata:
                    if re.compile(loadedconversion['TransactionRegex']).match(record.RawTransactionType):
                        record.ParsedTransactionType = loadedconversion['ParsedTransaction']
                        conversionFound = True
                        break
                if not conversionFound:
                    result = self._updateneededmethod(record)
                    record.ParsedTransactionType = result['transactiontype']
                    if 'saveregex' in result and result['saveregex']:
                        converterdata = {}
                        converterdata['ParsedTransaction'] = result['transactiontype']
                        converterdata['TransactionRegex'] = result['regex']
                        self._loadedregexdata.append(converterdata)
                        with open(self._regexfile, "w") as f:
                            filecontents = json.dumps(self._loadedregexdata)
                            f.write(filecontents)
            yield record