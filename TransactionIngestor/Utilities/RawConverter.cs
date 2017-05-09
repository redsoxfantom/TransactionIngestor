using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Utilities
{
    public class RawConverter : IDataRecordProducer, IDataRecordConsumer
    {
        Func<DataRecord, Tuple<DataRecord, Regex>> UpdateNeededMethod;
        private List<RawConverterData> loadedConverters;

        public IDataRecordProducer Producer
        {
            set;
            private get;
        }

        public RawConverter(Func<DataRecord,Tuple<DataRecord,Regex>> UpdateNeededMethod)
        {
            this.UpdateNeededMethod = UpdateNeededMethod;
            loadedConverters = RawConverterData.ReadConfig();
        }

        public IEnumerable<DataRecord> GetRecords()
        {
            foreach(var record in Producer.GetRecords())
            {
                bool foundConverter = false;
                foreach(var converter in loadedConverters)
                {
                    if(converter.TransactionRegex.IsMatch(record.RawTransactionType))
                    {
                        record.ParsedTransactionType = converter.ParsedTransaction;
                        foundConverter = true;
                        break;
                    }
                }

                if(!foundConverter)
                {
                    var updatedConfigRecord = UpdateNeededMethod(record);
                    loadedConverters.Add(new RawConverterData()
                    {
                        TransactionRegex = updatedConfigRecord.Item2,
                        ParsedTransaction = updatedConfigRecord.Item1.ParsedTransactionType
                    });
                    RawConverterData.WriteConfig(loadedConverters);
                    yield return updatedConfigRecord.Item1;
                }
                else
                {
                    yield return record;
                }
            }
        }

        private class RawConverterData
        {
            public String ParsedTransaction { get; set; }
            public Regex TransactionRegex { get; set; }

            public static List<RawConverterData> ReadConfig()
            {
                string configFile = Path.Combine(Directory.GetCurrentDirectory(),"rawConvertConfig.json");
                if (File.Exists(configFile))
                {
                    return JsonConvert.DeserializeObject<List<RawConverterData>>(File.ReadAllText(configFile), new RegexConverter());
                }
                else
                {
                    return new List<RawConverterData>();
                }
            }

            public static void WriteConfig(List<RawConverterData> config)
            {
                string configFile = Path.Combine(Directory.GetCurrentDirectory(), "rawConvertConfig.json");
                File.WriteAllText(configFile, JsonConvert.SerializeObject(config, new RegexConverter()));
            }
        }
    }
}
