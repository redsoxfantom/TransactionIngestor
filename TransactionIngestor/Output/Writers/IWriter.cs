using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output.Writers
{
    public interface IWriter<T> : IDataConsumer<T>
    {
        String FileToWriteTo { set; }

        void Start();
    }
}
