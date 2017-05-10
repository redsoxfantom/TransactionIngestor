using TransactionIngestor.Enums;
using TransactionIngestor.Output.Combiners.Exceptions;

namespace TransactionIngestor.Output.Combiners
{
    public class CombinerFactory
    {
        public static ICombiner<object> CreateCombiner(OutputType combinerType)
        {
            switch(combinerType)
            {
                default:
                    throw new CombinerException("Combiner could not be created to handle output type "+combinerType);
            }
        }
    }
}
