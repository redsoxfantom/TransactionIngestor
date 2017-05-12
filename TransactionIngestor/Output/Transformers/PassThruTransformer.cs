using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output.Transformers
{
    public class PassThruTransformer : ITransformer
    {
        public IDataProducer<DataRecord> Producer
        {
            set;
            private get;
        }

        public IEnumerable<object> GetRecords()
        {
            foreach(var record in Producer.GetRecords())
            {
                yield return record;
            }
        }
    }
}
