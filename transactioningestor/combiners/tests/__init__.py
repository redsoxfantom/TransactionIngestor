import unittest
import os
from unittest.mock import *
from transactioningestor.enums import OutputType
from transactioningestor.combiners import createcombiner
from transactioningestor.combiners.stdfmtjsoncombiner import StandardFormatJsonCombiner
from transactioningestor.combiners.passthrucombiner import PassThruCombiner

class IngestorTests(unittest.TestCase):
    def test_create_combiner_no_file(self):
        combiner = createcombiner(OutputType.STANDARD_FORMAT_JSON.name,"producer")
        self.assertEquals(combiner.__class__.__name__,"PassThruCombiner")

    @patch("os.path.isfile")
    def test_stdformatcombiner_filenotfound(self,isFileMock):
        isFileMock.return_value = False
        with self.assertRaises(Exception):
            StandardFormatJsonCombiner("producer","file")
