using TransactionIngestor.Enums;
using TransactionIngestor.Input.Ingestors.Exceptions;

namespace TransactionIngestor.Input.Ingestors
{
    public static class IngestorFactory
    {
        public static IIngestor CreateIngestor(InputType type)
        {
            switch(type)
            {
                case InputType.WELLS_FARGO_CSV:
                    return new WellsFargoCsvIngestor();
                case InputType.STANDARD_FORMAT_JSON:
                    return new StdFmtJsonIngestor();
                default:
                    throw new IngestorException("Ingestor could not be created to handle "+type);
            }
        }
    }
}
