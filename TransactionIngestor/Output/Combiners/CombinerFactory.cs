using TransactionIngestor.Combiners.Exceptions;
using TransactionIngestor.Enums;

namespace TransactionIngestor.Combiners
{
    public class CombinerFactory
    {
        public static ICombiner CreateCombiner(OutputType combinerType)
        {
            switch(combinerType)
            {
                default:
                    throw new CombinerException("Combiner could not be created to handle output type "+combinerType);
            }
        }
    }
}
