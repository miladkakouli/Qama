using System;
using Qama.Framework.Core.Abstractions.Types;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Core.Abstractions.Events
{
    public abstract class EventBase : IEvent, IValidatable
    {
        public DateTime EventDateTime { get; set; }
        protected EventBase() : base()
        {
            EventDateTime = DateTime.Now;
        }

        public abstract string GetRoutingKey();
    }
}
