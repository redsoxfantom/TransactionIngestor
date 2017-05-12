using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output.Transformers
{
    public interface ITransformer : IDataRecordConsumer, IDataProducer<object>
    {
    }
}
