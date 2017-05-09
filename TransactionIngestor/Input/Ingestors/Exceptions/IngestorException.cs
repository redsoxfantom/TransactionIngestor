using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Input.Ingestors.Exceptions
{
    public class IngestorException : Exception
    {
        public IngestorException()
        {
        }

        public IngestorException(string message) : base(message)
        {
        }

        public IngestorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IngestorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
