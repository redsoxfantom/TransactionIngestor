using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Output.Writers.Exceptions
{
    public class WriterException : Exception
    {
        public WriterException()
        {
        }

        public WriterException(string message) : base(message)
        {
        }

        public WriterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WriterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
