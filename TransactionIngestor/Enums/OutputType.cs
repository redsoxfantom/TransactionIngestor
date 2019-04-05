using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Enums
{
    public enum OutputType
    {
        STANDARD_FORMAT_JSON,
		MONTHLY_TOTALS_HUMAN_READABLE,
		MONTHLY_TOTALS_JSON,
		MONTHLY_TOTALS_EXCEL,
        MONTHLY_TOTALS_CSV,
		MONTHLY_AVERAGE_HUMAN_READABLE
    }
}
