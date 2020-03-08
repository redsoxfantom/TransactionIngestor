import os
from transactioningestor.data import get_datarecords_from_file

class StandardFormatJsonCombiner:
    def __init__(self,dataproducer,filetocombinewith):
        if not os.path.isfile(filetocombinewith):
            raise Exception(F"{filetocombinewith} not found")
        self._dataproducer = dataproducer
        self._filetocombinewith = filetocombinewith

    def get_records(self):
        for record in get_datarecords_from_file(self._filetocombinewith):
            yield record

        for record in self._dataproducer.get_records():
            yield record
