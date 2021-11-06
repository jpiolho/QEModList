using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QEModList.Core.Exceptions
{
    public class SourceException : Exception
    {
        public SourceException()
        {
        }

        public SourceException(string message) : base(message)
        {
        }

        public SourceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
