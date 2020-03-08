import unittest
import os
from unittest.mock import *
from transactioningestor.combiners import createcombiner
from transactioningestor.combiners.stdfmtjsoncombiner import StandardFormatJsonCombiner
from transactioningestor.combiners.passthrucombiner import PassThruCombiner

class IngestorTests(unittest.TestCase):
    def test_stdformatcombiner_filenotfound(self):
        os.path.isfile = MagicMock()
        os.path.isfile.return_value = False
        with self.assertRaises(Exception):
            StandardFormatJsonCombiner("producer","file")
