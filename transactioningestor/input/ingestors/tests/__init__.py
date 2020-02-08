from transactioningestor.input.ingestors import *
from transactioningestor.enums import InputType
from datetime import datetime
import uuid
import os
import unittest

class InitTests(unittest.TestCase):

    def test_nonexistantfile(self):
        with self.assertRaises(IngestorException):
            createingestor(InputType.WELLS_FARGO_CSV,str(uuid.uuid4()))

    def test_nonexistantinputtype(self):
        with self.assertRaises(IngestorException):
            createingestor("nonexistanttype",str(uuid.uuid4()))

    def test_wellsfargocsv(self):
        ingestor = createingestor(InputType.WELLS_FARGO_CSV.name,os.path.dirname(__file__)+"/dummy.csv")
        self.assertEqual(ingestor.__class__.__name__,"WellsFargoIngestor")
    
    def test_stdfmtjson(self):
        ingestor = createingestor(InputType.STANDARD_FORMAT_JSON.name,os.path.dirname(__file__)+"/dummy.csv")
        self.assertEqual(ingestor.__class__.__name__,"StandardFormatJsonIngestor")

class WellsFargoCsvTests(unittest.TestCase):

    def setUp(self):
        self.ingestor = WellsFargoIngestor(os.path.join(os.path.dirname(__file__),"dummy.csv"))

    def test_init(self):
        self.assertEqual(self.ingestor._inputfilename,os.path.join(os.path.dirname(__file__),"dummy.csv"))

class StdFormatJsonTests(unittest.TestCase):

    def setUp(self):
        self.ingestor = StandardFormatJsonIngestor(os.path.join(os.path.dirname(__file__),"dummy.json"))

    def test_init(self):
        self.assertEqual(self.ingestor._inputfilename,os.path.join(os.path.dirname(__file__),"dummy.json"))
    
    def test_get_records(self):
        for record in self.ingestor.get_elements():
            if record.RawTransactionType == 'MOBILE DEPOSIT':
                self.assertEqual('Transfer',record.ParsedTransactionType)
                self.assertEqual(300.0, record.TransactionAmount)
                self.assertEqual(datetime(2017,7,21,0,0),record.TransactionDate)
            if record.RawTransactionType == 'DEPOSIT':
                self.assertEqual('Deposit',record.ParsedTransactionType)
                self.assertEqual(11514, record.TransactionAmount)
                self.assertEqual(datetime(2017,8,25,0,0),record.TransactionDate)
