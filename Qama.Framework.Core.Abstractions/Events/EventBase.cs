using System;
using Qama.Framework.Core.Abstractions.Types;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Core.Abstractions.Events
{
    public abstract class EventBase : IEvent, IValidatable
    {
        public readonly DateTime EventDateTime;
        protected EventBase() : base()
        {
            EventDateTime = DateTime.Now;
        }

        public string GetRoutingKey()
        {
            throw new NotImplementedException();
        }
    }
}
