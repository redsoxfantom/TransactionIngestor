using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Output.Combiners
{
    public class StdFmtJsonCombiner : ICombiner
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
            using (FileStream fs = new FileStream(FileToCombineWith,FileMode.Open,FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                while(jsonReader.Read())
                {
                    if(jsonReader.TokenType == JsonToken.StartObject)
                    {
                        JObject obj = JObject.Load(jsonReader);
                        DataRecord record = obj.ToObject<DataRecord>();
                        yield return record;
                    }
                }
            }

            foreach (DataRecord record in Producer.GetRecords())
            {
                yield return record;
            }
        }
    }
}
