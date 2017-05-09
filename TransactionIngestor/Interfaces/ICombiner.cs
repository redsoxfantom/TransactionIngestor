using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Interfaces
{
    public interface ICombiner: IDataRecordConsumer, IDataRecordProducer
    {
        String FileToCombineWith { set; }
    }
}
