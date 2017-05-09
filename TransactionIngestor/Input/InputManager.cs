using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Input
{
    public class InputManager : IDataRecordProducer
    {
        public InputManager(Func<DataRecord, Tuple<DataRecord, Regex>> RawConverterUpdateNeeded)
        {

        }

        public IEnumerable<DataRecord> GetRecords()
        {
            throw new NotImplementedException();
        }
    }
}
