using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qama.Framework.Core.Abstractions.Types;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Core.Abstractions.Events
{
    public abstract class EventBase : IEvent, IValidatable
    {
        public DateTime EventDateTime { get; set; }
        protected EventBase(bool persistent = true, byte priority = Byte.MinValue) : base()
        {
            _persistent = persistent;
            _priority = priority;
            EventDateTime = DateTime.Now;
        }

        public abstract string GetRoutingKey();
        public virtual IDictionary<string, object> GetHeaders()
        {
            return new Dictionary<string, object>();
        }

        public virtual IDictionary<string, object> GetQueueArgs()
        {
            return new Dictionary<string, object>();
        }
        protected readonly bool _persistent;
        protected readonly byte _priority;
        public bool Persistent() => _persistent;
        public byte Priority() => _priority;
    }
}
