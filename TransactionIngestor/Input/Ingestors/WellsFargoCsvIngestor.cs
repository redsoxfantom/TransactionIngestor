﻿using CsvHelper;
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
                while(csv.Read())
                {
                    var record = csv.GetRecord<WellsFargoCsv>();
                    var dataRecord = new DataRecord()
                    {
                        RawTransactionType = record.Desc,
                        TransactionAmount = decimal.Parse(record.Amt),
                        TransactionDate = DateTime.ParseExact(record.Date, "d", CultureInfo.InvariantCulture)
                    };

                    yield return dataRecord;
                }
            }
        }

        class WellsFargoCsv
        {
            public String Date { get; set; }
            public String Amt { get; set; }
            public String Asterisk { get; set; }
            public String Empty { get; set; }
            public String Desc { get; set; }
        }
    }
}