from transactioningestor.data import DataRecord
from transactioningestor.data import get_datarecords_from_file

class StandardFormatJsonIngestor(object):
    
    def __init__(self,inputfilename):
        self._inputfilename = inputfilename

    def get_elements(self):
        for record in get_datarecords_from_file(self._inputfilename):
            yield record
        