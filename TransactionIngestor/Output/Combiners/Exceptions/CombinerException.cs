using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TransactionIngestor.Combiners.Exceptions
{
    public class CombinerException : Exception
    {
        public CombinerException()
        {
        }

        public CombinerException(string message) : base(message)
        {
        }

        public CombinerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CombinerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
