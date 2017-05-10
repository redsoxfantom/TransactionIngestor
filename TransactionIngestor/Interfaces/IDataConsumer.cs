using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Interfaces
{
    public interface IDataConsumer<T>
    {
        IDataProducer<T> Producer { set; }
    }
}
