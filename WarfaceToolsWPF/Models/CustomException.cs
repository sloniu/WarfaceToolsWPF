using System;
using System.Runtime.Serialization;

namespace WarfaceToolsWPF.Models
{
    [Serializable]
    internal class CustomException : Exception
    {
        public readonly int ErrorCode;
        
        public CustomException(string message, int code)
            : base(message)
        {
            ErrorCode = code;
        }

        public CustomException(string message, Exception innerException, int code)
            : base(message, innerException)
        {
            ErrorCode = code;
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
