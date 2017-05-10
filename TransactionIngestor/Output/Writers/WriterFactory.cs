using System;
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
        public static IWriter<object> CreateWriter(OutputType outputType)
        {
            switch(outputType)
            {
                default:
                    throw new WriterException("Could not generate writer for "+outputType);
            }
        }
    }
}
