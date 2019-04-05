using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Enums;

namespace TransactionIngestor.Output.Transformers
{
    public class TransformerFactory
    {
        public static ITransformer CreateTransformer(OutputType outputType)
        {
            switch(outputType)
            {
				case OutputType.MONTHLY_TOTALS_HUMAN_READABLE:
				case OutputType.MONTHLY_TOTALS_JSON:
				case OutputType.MONTHLY_TOTALS_EXCEL:
                case OutputType.MONTHLY_TOTALS_CSV:
					return new MonthlyStatisticsTransform ();
                default:
                    return new PassThruTransformer();
            }
        }
    }
}
