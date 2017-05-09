using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Enums;
using TransactionIngestor.Exceptions;
using TransactionIngestor.Interfaces;

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
