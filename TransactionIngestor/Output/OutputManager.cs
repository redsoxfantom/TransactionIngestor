using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Combiners;
using TransactionIngestor.Data;
using TransactionIngestor.Enums;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output
{
    public class OutputManager : IDataRecordConsumer
    {
        ICombiner<object> mCombiner;

        public IDataProducer<DataRecord> Producer
        {
            set;
            private get;
        }

        public OutputManager(string outputFile, bool combine, OutputType outputType)
        {

        }
    }
}
