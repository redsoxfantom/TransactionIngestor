from transactioningestor.input.ingestors import createingestor
from transactioningestor.input.dataconverter import RawConverter
from transactioningestor.input.extraneoustag import ExtraneousTagHandler
from transactioningestor import get_transaction_dir
import os

class InputManager:
    def __init__(self, ingestortype, inputfile, updateneededmethod, extraneoustag = None):
        self._ingestor = createingestor(ingestortype, inputfile)
        self._conversionfile = os.path.join(get_transaction_dir(),"rawConvertConfig.json")
        self._rawconverter = RawConverter(self._ingestor,updateneededmethod,self._conversionfile)
        self._extraneoustaghandler = ExtraneousTagHandler(self._rawconverter,extraneoustag)

    def get_records(self):
        for record in self._extraneoustaghandler.get_records():
            yield record
