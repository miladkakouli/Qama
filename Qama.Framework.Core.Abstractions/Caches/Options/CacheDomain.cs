using Qama.Framework.Core.Abstractions.Types;

namespace Qama.Framework.Core.Abstractions.Caches.Options
{
    public abstract class CacheDomain : Enumeration
    {
        public static CacheDomain DefaultCache { get; private set; }
        protected CacheDomain(int id, string name) : base(id, name)
        { }
    }
}
