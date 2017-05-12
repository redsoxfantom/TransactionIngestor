using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;
using TransactionIngestor.Output.Combiners;

namespace TransactionIngestor.Output.Writers
{
    public class StdFmtJsonWriter : IWriter
    {
        public string FileToWriteTo
        {
            set;
            private get;
        }
        public IDataProducer<object> Producer
        {
            set;
            private get;
        }

        public void Start()
        {

        }
    }
}
