using System;
using System.Runtime.Serialization;

namespace Qama.Framework.Core.Abstractions.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessExceptionDomain DomainException { get; private set; }
        public BusinessException(BusinessExceptionDomain domain) : base()
            => DomainException = domain;
        public BusinessException(BusinessExceptionDomain domain, SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext) => DomainException = domain;
        public BusinessException(BusinessExceptionDomain domain, string message) :
            base(message) => DomainException = domain;
        public BusinessException(BusinessExceptionDomain domain, string message, Exception innerException) :
            base(message, innerException) => DomainException = domain;
    }
}
