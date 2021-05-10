using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Qama.Framework.Core.Abstractions.Events;

namespace Qama.Framework.Core.EventBus.MediatR
{
    public class MediatRRequestEventHandler<T> : INotificationHandler<MediatREventBase<T>> where T : EventBase
    {
        private readonly IEnumerable<IEventHandler<T>> _eventHandlers;

        public MediatRRequestEventHandler(IEnumerable<IEventHandler<T>> eventHandlers)
        {
            _eventHandlers = eventHandlers;
        }

        public async Task Handle(MediatREventBase<T> notification, CancellationToken cancellationToken)
        {
            foreach (var eventHandler in _eventHandlers)
            {
                await eventHandler.Handle(notification.Event);
            }
        }
    }
}
