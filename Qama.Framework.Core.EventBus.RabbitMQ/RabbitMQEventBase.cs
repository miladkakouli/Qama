using System;
using System.Linq;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Extensions.Serializer;
using RabbitMQ.Client.Events;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQEventBase<T> : BasicDeliverEventArgs where T : EventBase
    {
        public RabbitMQEventBase()
        { }
    }
}
