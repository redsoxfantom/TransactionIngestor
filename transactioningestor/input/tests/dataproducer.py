from transactioningestor.data import DataRecord

class DataProducer:
    def __init__(self,RawTransactionType,ParsedTransactionType):
        self.ParsedTransactionType = ParsedTransactionType
        self.RawTransactionType = RawTransactionType
    def get_records(self):
        yield DataRecord(self.RawTransactionType,self.ParsedTransactionType,None,None)