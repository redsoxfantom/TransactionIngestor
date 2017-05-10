using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;

namespace TransactionIngestor.Interfaces
{
    public interface IDataProducer<out T>
    {
        IEnumerable<T> GetRecords();
    }
}
