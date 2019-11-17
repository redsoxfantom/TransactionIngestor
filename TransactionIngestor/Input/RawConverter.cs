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

namespace TransactionIngestor.Input
{
    public class RawConverter : IDataRecordProducer, IDataRecordConsumer
    {
        Func<DataRecord, Tuple<DataRecord, Regex>> UpdateNeededMethod;
        private List<RawConverterData> loadedConverters;

        public IDataProducer<DataRecord> Producer
        {
            set;
            private get;
        }

        public RawConverter(Func<DataRecord,Tuple<DataRecord,Regex>> UpdateNeededMethod)
        {
            this.UpdateNeededMethod = UpdateNeededMethod;
            loadedConverters = RawConverterData.ReadConfig();
        }

        public List<RawConverterData> GetLoadedConverters()
        {
            return loadedConverters;
        }

        public IEnumerable<DataRecord> GetRecords()
        {
            foreach(var record in Producer.GetRecords())
            {
                bool foundConverter = false;
                if (String.IsNullOrEmpty(record.ParsedTransactionType))
                {
                    foreach (var converter in loadedConverters)
                    {
                        if (converter.TransactionRegex.IsMatch(record.RawTransactionType))
                        {
                            record.ParsedTransactionType = converter.ParsedTransaction;
                            foundConverter = true;
                            break;
                        }
                    }

                    if (!foundConverter)
                    {
                        var updatedConfigRecord = UpdateNeededMethod(record);
                        if(updatedConfigRecord.Item2 != null)
                        {
                            loadedConverters.Add(new RawConverterData()
                            {
                                TransactionRegex = updatedConfigRecord.Item2,
                                ParsedTransaction = updatedConfigRecord.Item1.ParsedTransactionType
                            });
                            RawConverterData.WriteConfig(loadedConverters);
                        }
                        yield return updatedConfigRecord.Item1;
                    }
                    else
                    {
                        yield return record;
                    }
                }
                else
                {
                    yield return record;
                }
            }
        }
    }
}
