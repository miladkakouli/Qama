using System;
using System.Linq;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.ServiceLocator;

namespace Qama.Framework.Core.EventBus.InMemory
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceLocator _locator;
        private readonly IEverythingLogger _everythingLogger;
        public InMemoryEventBus(IServiceLocator locator, IEverythingLogger everythingLogger)
        {
            _locator = locator;
            _everythingLogger = everythingLogger;
            _everythingLogger.LogDebug($"using {nameof(InMemoryEventBus)}");
        }

        public async Task Publish<T>(T @event)
            where T : EventBase
        {
            var handlers = _locator.GetInstances<IEventHandler<T>>();
            _everythingLogger.LogDebug($"Found {handlers.Count()} handler for {@event}");
            foreach (var handler in handlers)
            {
                _everythingLogger.LogDebug($"calling {handler} handler for {@event}");
                await handler.Handle(@event);
            }
        }

        public void Subscribe<T, TEventHandler>()
            where T : EventBase where TEventHandler : IEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Type type)
            where T : EventBase
        {
            throw new NotImplementedException();
        }
    }
}
