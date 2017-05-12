using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output.Combiners
{
    public interface ICombiner : IDataConsumer<object>, IDataProducer<object>
    {
        String FileToCombineWith { set; }
    }
}
