import unittest
import os
from unittest.mock import *
from transactioningestor import get_transaction_dir
from pathlib import Path

class TransactionDirTests(unittest.TestCase):
    def test_get_transaction_dir_env_val(self):
        os.environ["TRANSACTIONS_DIR"] = "/path/to/transactions"
        os.path.isfile = MagicMock()
        os.path.isfile.return_value = False
        os.path.isdir = MagicMock()
        os.path.isdir.return_value = False
        os.makedirs = MagicMock()

        transactiondir = get_transaction_dir()

        self.assertEqual("/path/to/transactions",transactiondir)
        os.makedirs.assert_called_once_with("/path/to/transactions")

    def test_get_transaction_dir_no_env_val(self):
        del os.environ["TRANSACTIONS_DIR"]
        Path.home = MagicMock()
        Path.home.return_value = "/home/transactions"
        os.path.isdir = MagicMock()
        os.path.isdir.return_value = False
        os.makedirs = MagicMock()

        transactiondir = get_transaction_dir()

        self.assertEqual("/home/transactions/Transactions",transactiondir)
        os.makedirs.assert_called_once_with("/home/transactions/Transactions")
