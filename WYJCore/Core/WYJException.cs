using System;
using System.Runtime.Serialization;

namespace WYJCore
{
    [Serializable]
    public class WYJException : Exception
    {
        public WYJException()
        {
        }

        public WYJException(string message) : base(message)
        {
        }

        public WYJException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WYJException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}