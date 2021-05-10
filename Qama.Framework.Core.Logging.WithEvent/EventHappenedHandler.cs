using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;

namespace Qama.Framework.Core.Logging.WithEvent
{
    public class EventHappenedHandler : IEventHandler<EventHappened>
    {
        private readonly IEverythingLogger _logger;

        public EventHappenedHandler(IEverythingLogger logger)
        {
            _logger = logger;
        }

        public Task Handle(EventHappened @event)
        {
            switch (@event.EventType)
            {
                case EventType.Trace:
                    _logger.LogTrace(@event.EventId, @event.Exception, @event.Message, @event.Args, @event.EventDateTime);
                    break;
                case EventType.Debug:
                    _logger.LogDebug(@event.EventId, @event.Exception, @event.Message, @event.Args, @event.EventDateTime);
                    break;
                case EventType.Information:
                    _logger.LogInformation(@event.EventId, @event.Exception, @event.Message, @event.Args, @event.EventDateTime);
                    break;
                case EventType.Warning:
                    _logger.LogWarning(@event.EventId, @event.Exception, @event.Message, @event.Args, @event.EventDateTime);
                    break;
                case EventType.Error:
                    _logger.LogError(@event.EventId, @event.Exception, @event.Message, @event.Args, @event.EventDateTime);
                    break;
                case EventType.Critical:
                    _logger.LogCritical(@event.EventId, @event.Exception, @event.Message, @event.Args, @event.EventDateTime);
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
