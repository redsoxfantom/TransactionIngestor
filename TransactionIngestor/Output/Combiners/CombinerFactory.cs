using TransactionIngestor.Enums;
using TransactionIngestor.Output.Combiners.Exceptions;

namespace TransactionIngestor.Output.Combiners
{
    public class CombinerFactory
    {
        public static ICombiner CreateCombiner(OutputType combinerType)
        {
            switch(combinerType)
            {
                case OutputType.STANDARD_FORMAT_JSON:
                    return new StdFmtJsonCombiner();
                default:
                    throw new CombinerException("Combiner could not be created to handle output type "+combinerType);
            }
        }
    }
}
