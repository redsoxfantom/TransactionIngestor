using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Data
{
    public class DataRecord
    {
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public string RawTransactionType { get; set; }
        public string ParsedTransactionType { get; set; }
    }
}
