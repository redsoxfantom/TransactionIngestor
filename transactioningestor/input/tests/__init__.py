from transactioningestor.input.dataconverter import RawConverter
from transactioningestor.data import DataRecord
import unittest

class DataProducer:
    def __init__(self,ParsedTransactionType):
        self.ParsedTransactionType = ParsedTransactionType
    def get_records(self):
        yield DataRecord(None,self.ParsedTransactionType,None,None)

class RawConverterTests(unittest.TestCase):

    def test_init(self):
        converter = RawConverter("producer","method")
        self.assertEqual("producer",converter._dataproducer)
        self.assertEqual("method",converter._updateneededmethod)

    def test_no_conversion_needed(self):
        converter = RawConverter(DataProducer("Data"),None)
        for record in converter.get_records():
            self.assertEqual("Data",record.ParsedTransactionType)
        