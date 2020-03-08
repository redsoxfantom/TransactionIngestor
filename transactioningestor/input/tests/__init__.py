from transactioningestor.input.dataconverter import RawConverter
from transactioningestor.input.extraneoustag import ExtraneousTagHandler
from transactioningestor.data import DataRecord
import os
import unittest
import json
from transactioningestor.input.tests.dataproducer import DataProducer
from unittest.mock import *

class RawConverterTests(unittest.TestCase):

    @patch('os.path.isfile')
    def test_init(self,mockIsFile):
        mockIsFile.return_value = False
        converter = RawConverter("producer","method","file")
        self.assertEqual("producer",converter._dataproducer)
        self.assertEqual("method",converter._updateneededmethod)
        self.assertEqual(0,len(converter._loadedregexdata))

    @patch('os.path.isfile')
    def test_no_conversion_needed(self,mockIsFile):
        mockIsFile.return_value = False
        converter = RawConverter(DataProducer("RawData","Data"),None,"file")
        for record in converter.get_records():
            self.assertEqual("Data",record.ParsedTransactionType)
            self.assertEqual("RawData",record.RawTransactionType)

    def test_regex_conversion(self):
        converter = RawConverter(DataProducer("DataForTesting",None),"method",os.path.join(os.path.dirname(__file__),"convert.json"))
        for record in converter.get_records():
            self.assertEqual("DataForTesting",record.RawTransactionType)
            self.assertEqual("Custom Parsed Transaction", record.ParsedTransactionType)

    @patch('os.path.isfile')
    def test_regex_doesnt_match(self,mockIsFile):
        mockIsFile.return_value = False
        mockopen = mock_open()
        with patch(F"{RawConverter.__module__}.open",mockopen):
            converter = RawConverter(DataProducer("UNRECOGNIZEDDATA",None),lambda record: ("NEWREGEX","NEWDATA"),"/dummy/file.json")
            for record in converter.get_records():
                self.assertEqual("UNRECOGNIZEDDATA",record.RawTransactionType)
                self.assertEqual("NEWDATA",record.ParsedTransactionType)
            mockopen.assert_called_once_with("/dummy/file.json",'w')
            mockopen().write.assert_called_once_with("[{\"ParsedTransaction\": \"NEWDATA\", \"TransactionRegex\": \"NEWREGEX\"}]")
        
    @patch('os.path.isfile')
    def test_regex_doesnt_match_no_new_regex(self,mockIsFile):
        mockIsFile.return_value = False
        mockopen = mock_open()
        with patch(F"{RawConverter.__module__}.open",mockopen):
            converter = RawConverter(DataProducer("UNRECOGNIZEDDATA",None),lambda record: (None,"NEWDATA"),"/dummy/file.json")
            for record in converter.get_records():
                self.assertEqual("UNRECOGNIZEDDATA",record.RawTransactionType)
                self.assertEqual("NEWDATA",record.ParsedTransactionType)
            mockopen.assert_not_called()
            mockopen().write.assert_not_called()
            
    @patch('os.path.isfile')
    def test_regex_doesnt_match_add_regex(self,mockIsFile):
        mockIsFile.return_value = True
        mockopen = mock_open(read_data="[{\"ParsedTransaction\": \"NEWDATA\", \"TransactionRegex\": \"NEWREGEX\"}]")
        with patch(F"{RawConverter.__module__}.open",mockopen):
            converter = RawConverter(DataProducer("DataForTesting",None),lambda record: ("NEWREGEX2","NEWDATA2"), "/dummy/file.json")
            for record in converter.get_records():
                self.assertEqual("DataForTesting",record.RawTransactionType)
                self.assertEqual("NEWDATA2", record.ParsedTransactionType)
            mockopen().write.assert_called_once_with('[{"ParsedTransaction": "NEWDATA", "TransactionRegex": "NEWREGEX"}, {"ParsedTransaction": "NEWDATA2", "TransactionRegex": "NEWREGEX2"}]')

    def test_extraneoustag_not_found(self):
        handler = ExtraneousTagHandler(DataProducer("RawData","Data"),extraneoustag="NOTHING")
        for record in handler.get_records():
            self.assertEqual("RawData",record.RawTransactionType)
            self.assertEqual("Data", record.ParsedTransactionType)

    def test_no_extraneoustag(self):
        handler = ExtraneousTagHandler(DataProducer("RawData","Data"))
        for record in handler.get_records():
            self.assertEqual("RawData",record.RawTransactionType)
            self.assertEqual("Data", record.ParsedTransactionType)

    def test_extraneoustag_found(self):
        handler = ExtraneousTagHandler(DataProducer("RawData","TAGData"),extraneoustag="TAG")
        count = 0
        for record in handler.get_records():
            count += 1
        self.assertEqual(0,count)