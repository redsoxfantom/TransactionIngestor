using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            JsonSerializer ser = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(FileToWriteTo))
            using (JsonWriter jsonWriter = new JsonTextWriter(writer))
            {
                foreach (DataRecord record in Producer.GetRecords())
                {
                    ser.Serialize(jsonWriter, record);
                }
            }
        }
    }
}
