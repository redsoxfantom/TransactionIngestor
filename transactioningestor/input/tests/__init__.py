from transactioningestor.input.dataconverter import *
from transactioningestor.data import DataRecord
import os
import unittest
import tempfile
import json

class DataProducer:
    def __init__(self,RawTransactionType,ParsedTransactionType):
        self.ParsedTransactionType = ParsedTransactionType
        self.RawTransactionType = RawTransactionType
    def get_records(self):
        yield DataRecord(self.RawTransactionType,self.ParsedTransactionType,None,None)

class RawConverterTests(unittest.TestCase):

    def test_init(self):
        converter = RawConverter("producer","method","file")
        self.assertEqual("producer",converter._dataproducer)
        self.assertEqual("method",converter._updateneededmethod)
        self.assertEqual(0,len(converter._loadedregexdata))

    def test_no_conversion_needed(self):
        converter = RawConverter(DataProducer("RawData","Data"),None,"file")
        for record in converter.get_records():
            self.assertEqual("Data",record.ParsedTransactionType)
            self.assertEqual("RawData",record.RawTransactionType)

    def test_regex_conversion(self):
        converter = RawConverter(DataProducer("DataForTesting",None),"method",os.path.join(os.path.dirname(__file__),"convert.json"))
        for record in converter.get_records():
            self.assertEqual("DataForTesting",record.RawTransactionType)
            self.assertEqual("Custom Parsed Transaction", record.ParsedTransactionType)

    def test_regex_doesnt_match(self):
        tempdir = tempfile.mkdtemp()
        testtempfile = os.path.join(tempdir,"temp.json")
        converter = RawConverter(DataProducer("UNRECOGNIZEDDATA",None),lambda record: ("NEWREGEX","NEWDATA"),testtempfile)
        for record in converter.get_records():
            self.assertEqual("UNRECOGNIZEDDATA",record.RawTransactionType)
            self.assertEqual("NEWDATA",record.ParsedTransactionType)
        with open(testtempfile) as f:
            print(f.read())