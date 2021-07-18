using System;
using System.Collections.Generic;
using System.Linq;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Extensions.Serializer;
using RabbitMQ.Client.Events;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public abstract class RabbitMQEventBase : EventBase
    {
        public RabbitMQEventBase()
        { }
        public virtual IDictionary<string, object> GetHeaders()
        {
            return new Dictionary<string, object>()
            {
                {"deploy_mode", 2}
            };
        }
    }
}
