using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output.Combiners
{
    public class PassThruCombiner : ICombiner
    {
        public string FileToCombineWith
        {
            set;
            private get;
        }

        public IDataProducer<object> Producer
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
