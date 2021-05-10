using Qama.Framework.Core.Abstractions.Types;

namespace Qama.Framework.Core.Abstractions.Logging
{
    public abstract class LogEventId : Enumeration
    {
        public static LogEventId DefaultEvent { get; private set; }
        protected LogEventId(int id, string name) : base(id, name)
        { }
    }
}
