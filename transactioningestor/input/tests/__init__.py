from transactioningestor.input.dataconverter import *
from transactioningestor.data import DataRecord
import os
import unittest
import json
from unittest.mock import *

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
        os.path.isfile = MagicMock()
        os.path.isfile.return_value = False
        mockopen = mock_open()
        with patch(F"{RawConverter.__module__}.open",mockopen):
            converter = RawConverter(DataProducer("UNRECOGNIZEDDATA",None),lambda record: ("NEWREGEX","NEWDATA"),"/dummy/file.json")
            for record in converter.get_records():
                self.assertEqual("UNRECOGNIZEDDATA",record.RawTransactionType)
                self.assertEqual("NEWDATA",record.ParsedTransactionType)
            mockopen.assert_called_once_with("/dummy/file.json",'w')
            mockopen().write.assert_called_once_with("[{\"ParsedTransaction\": \"NEWDATA\", \"TransactionRegex\": \"NEWREGEX\"}]")
        
    def test_regex_doesnt_match_no_new_regex(self):
        os.path.isfile = MagicMock()
        os.path.isfile.return_value = False
        mockopen = mock_open()
        with patch(F"{RawConverter.__module__}.open",mockopen):
            converter = RawConverter(DataProducer("UNRECOGNIZEDDATA",None),lambda record: (None,"NEWDATA"),"/dummy/file.json")
            for record in converter.get_records():
                self.assertEqual("UNRECOGNIZEDDATA",record.RawTransactionType)
                self.assertEqual("NEWDATA",record.ParsedTransactionType)
            mockopen.assert_not_called()
            mockopen().write.assert_not_called()
            
    def test_regex_doesnt_match_add_regex(self):
        os.path.isfile = MagicMock()
        os.path.isfile.return_value = True
        mockopen = mock_open(read_data="[{\"ParsedTransaction\": \"NEWDATA\", \"TransactionRegex\": \"NEWREGEX\"}]")
        with patch(F"{RawConverter.__module__}.open",mockopen):
            mockopen().read
            converter = RawConverter(DataProducer("DataForTesting",None),lambda record: ("NEWREGEX2","NEWDATA2"), "/dummy/file.json")
            for record in converter.get_records():
                self.assertEqual("DataForTesting",record.RawTransactionType)
                self.assertEqual("NEWDATA2", record.ParsedTransactionType)
            mockopen().write.assert_called_once_with('[{"ParsedTransaction": "NEWDATA", "TransactionRegex": "NEWREGEX"}, {"ParsedTransaction": "NEWDATA2", "TransactionRegex": "NEWREGEX2"}]')
