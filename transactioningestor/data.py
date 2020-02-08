from dataclasses import dataclass
from datetime import datetime

@dataclass
class DataRecord:
    RawTransactionType: str
    ParsedTransactionType: str
    TransactionAmount: float
    TransactionDate: datetime