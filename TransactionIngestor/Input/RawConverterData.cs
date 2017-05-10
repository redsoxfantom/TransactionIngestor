using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TransactionIngestor.Input
{
    public class RawConverterData
    {
        public String ParsedTransaction { get; set; }
        public Regex TransactionRegex { get; set; }

        public static List<RawConverterData> ReadConfig()
        {
            string configFile = Path.Combine(Directory.GetCurrentDirectory(), "rawConvertConfig.json");
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
