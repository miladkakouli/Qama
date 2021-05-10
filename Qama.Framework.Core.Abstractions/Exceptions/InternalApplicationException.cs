using System;
using System.Runtime.Serialization;

namespace Qama.Framework.Core.Abstractions.Exceptions
{
    public class InternalApplicationException : Exception
    {
        public InternalApplicationExceptionDomain DomainException { get; private set; }
        public InternalApplicationException(InternalApplicationExceptionDomain domain) : base()
            => DomainException = domain;
        public InternalApplicationException(InternalApplicationExceptionDomain domain, SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext) => DomainException = domain;
        public InternalApplicationException(InternalApplicationExceptionDomain domain, string message) :
            base(message) => DomainException = domain;
        public InternalApplicationException(InternalApplicationExceptionDomain domain, string message, Exception innerException) :
            base(message, innerException) => DomainException = domain;
    }
}
