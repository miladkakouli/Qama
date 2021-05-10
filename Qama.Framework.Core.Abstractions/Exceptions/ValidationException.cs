using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Qama.Framework.Core.Abstractions.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationExceptionDomain> DomainExceptions { get; private set; }

        public ValidationException(params ValidationExceptionDomain[] domainExceptions) : base() => DomainExceptions = domainExceptions;
        public ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext, params ValidationExceptionDomain[] domainExceptions) :
            base(serializationInfo, streamingContext) =>
            DomainExceptions = domainExceptions;

        public ValidationException(string message, params ValidationExceptionDomain[] domainExceptions) :
            base(message) =>
            DomainExceptions = domainExceptions;

        public ValidationException(string message, Exception innerException, params ValidationExceptionDomain[] domainExceptions) :
            base(message, innerException) =>
            DomainExceptions = domainExceptions;
    }
}
