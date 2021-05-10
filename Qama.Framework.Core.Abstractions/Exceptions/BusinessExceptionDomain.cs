using Qama.Framework.Core.Abstractions.Types;

namespace Qama.Framework.Core.Abstractions.Exceptions
{
    public abstract class BusinessExceptionDomain : Enumeration
    {
        public static BusinessExceptionDomain DefaultBusinessException { get; private set; }
        protected BusinessExceptionDomain(int id, string name)
            : base(id, name) { }
    }
}
