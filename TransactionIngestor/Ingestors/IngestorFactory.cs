using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionIngestor.Exceptions;
using TransactionIngestor.Interfaces;

namespace TransactionIngestor.Ingestors
{
    public static class IngestorFactory
    {
        public static IIngestor CreateIngestor(InputType type)
        {
            switch(type)
            {
                case InputType.WELLS_FARGO_CSV:
                    return new WellsFargoCsvIngestor();
                default:
                    throw new IngestorException("Ingestor could not be created to handle "+type);
            }
        }
    }
}
