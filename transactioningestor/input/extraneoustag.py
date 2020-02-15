

class ExtraneousTagHandler:
    def __init__(self,dataproducer,extraneoustag=None):
        self._extraneoustag = extraneoustag
        self._dataproducer = dataproducer

    def get_records(self):
        for record in self._dataproducer.get_records():
            if not self._extraneoustag:
                yield record
            elif not record.ParsedTransactionType.startswith(self._extraneoustag):
                yield record