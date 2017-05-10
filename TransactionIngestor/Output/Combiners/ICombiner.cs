using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Combiners
{
    public interface ICombiner<T> : IDataConsumer<T>, IDataProducer<T>
    {
        String FileToCombineWith { set; }
    }
}
