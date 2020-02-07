from transactioningestor.input.ingestors import *
from transactioningestor.enums import ImputType
import unittest

class InitTests(unittest.TestCase):

    def test_nonexistantfile(self):
        with self.assertRaises(IngestorException):
            createingestor(ImputType.WELLS_FARGO_CSV,"/nonexistantfile")

    def test_nonexistantinputtype(self):
        with self.assertRaises(IngestorException):
            createingestor("nonexistanttype","/nonexistantfile")
