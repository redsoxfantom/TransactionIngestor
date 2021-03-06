﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Enums;
using TransactionIngestor.Output.Writers.Exceptions;

namespace TransactionIngestor.Output.Writers
{
    public class WriterFactory
    {
        public static IWriter CreateWriter(OutputType outputType)
        {
            switch(outputType)
            {
                case OutputType.STANDARD_FORMAT_JSON:
                    return new StdFmtJsonWriter();
				case OutputType.MONTHLY_TOTALS_HUMAN_READABLE:
					return new MonTotHumReadWriter ();
				case OutputType.MONTHLY_TOTALS_JSON:
					return new MonTotJsonWriter ();
				case OutputType.MONTHLY_TOTALS_EXCEL:
					return new MonTotExcelWriter ();
                case OutputType.MONTHLY_TOTALS_CSV:
                    return new MonTotCsvWriter();
                default:
                    throw new WriterException("Could not generate writer for "+outputType);
            }
        }
    }
}
