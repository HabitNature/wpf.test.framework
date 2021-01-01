using System;
using System.Runtime.Serialization;
using System.Security;

namespace Wpf.Test.Framework.Exceptions
{
    [Serializable]
    public class WindowNotFoundException : Exception
    {
        public WindowNotFoundException() : base()
        {
        }

        public WindowNotFoundException(string message) : base(message)
        {
        }

        public WindowNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected WindowNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
