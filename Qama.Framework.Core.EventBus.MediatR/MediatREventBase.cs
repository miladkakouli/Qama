using MediatR;
using Qama.Framework.Core.Abstractions.Events;

namespace Qama.Framework.Core.EventBus.MediatR
{
    public class MediatREventBase<T> : INotification where T : EventBase
    {
        public T Event { get; private set; }
        public MediatREventBase(T @event)
        {
            Event = @event;
        }
    }
}
