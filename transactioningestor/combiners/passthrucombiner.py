

class PassThruCombiner:
    def __init__(self,dataproducer):
        self._dataproducer = dataproducer

    def get_records(self):
        for record in self._dataproducer.get_records():
            yield record
            