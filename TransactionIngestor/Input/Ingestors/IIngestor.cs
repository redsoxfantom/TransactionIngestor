using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Input.Ingestors
{
    public interface IIngestor : IDataRecordProducer
    {
        String InputFileName { set; }
    }
}
