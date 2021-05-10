using System;
using System.Threading.Tasks;
using MediatR;
using Qama.Framework.Core.Abstractions.Events;

namespace Qama.Framework.Core.EventBus.MediatR
{
    public class MediatREventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public MediatREventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<T>(T @event)
            where T : EventBase
        {
            await _mediator.Publish(new MediatREventBase<T>(@event));
        }

        public void Subscribe<T, TEventHandler>()
            where T : EventBase where TEventHandler : IEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Type type) where T : EventBase
        {
            throw new NotImplementedException();
        }
    }
}
