using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;

namespace TransactionIngestor.Input.Ingestors
{
    public class StdFmtJsonIngestor : IIngestor
    {
        public string InputFileName
        {
            set;
            private get;
        }

        public IEnumerable<DataRecord> GetRecords()
        {
            using (FileStream fs = new FileStream(InputFileName, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        JObject obj = JObject.Load(jsonReader);
                        DataRecord record = obj.ToObject<DataRecord>();
                        yield return record;
                    }
                }
            }
        }
    }
}
