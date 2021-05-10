using Qama.Framework.Core.Abstractions.Types;

namespace Qama.Framework.Core.Abstractions.Exceptions
{
    public abstract class InternalApplicationExceptionDomain : Enumeration
    {
        public static InternalApplicationExceptionDomain DefaultApplicationException { get; private set; }
        protected InternalApplicationExceptionDomain(int id, string name) : base(id, name)
        { }
    }
}
