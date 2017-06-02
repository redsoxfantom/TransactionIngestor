using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Data;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Input.Ingestors
{
    public class WellsFargoCsvIngestor : IIngestor
    {
        public string InputFileName
        {
            set;
            private get;
        }

        public IEnumerable<DataRecord> GetRecords()
        {
			using (var csv = new CsvReader(File.OpenText(InputFileName)))
            {
				csv.Configuration.HasHeaderRecord = false;
                while(csv.Read())
                {
                    var record = new WellsFargoCsv();
                    record.Date = csv.GetField<string>(0);
                    record.Amt = csv.GetField<decimal>(1);
                    record.Desc = csv.GetField<String>(4);

                    var dataRecord = new DataRecord()
                    {
                        RawTransactionType = record.Desc,
                        TransactionAmount = record.Amt,
                        TransactionDate = DateTime.ParseExact(record.Date, "d", CultureInfo.InvariantCulture)
                    };

                    yield return dataRecord;
                }
            }
        }

        class WellsFargoCsv
        {
            public String Date { get; set; }
            public decimal Amt { get; set; }
            public String Desc { get; set; }
        }
    }
}
