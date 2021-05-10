using Qama.Framework.Core.Abstractions.Types;

namespace Qama.Framework.Core.Abstractions.Exceptions
{
    public abstract class ValidationExceptionDomain : Enumeration
    {
        public static ValidationExceptionDomain DefaultValidationException { get; private set; }
        public static ValidationExceptionDomain SignValidationException { get; private set; }

        protected ValidationExceptionDomain(int id, string name)
            : base(id, name) { }
    }
}
