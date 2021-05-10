using System;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;

namespace Qama.Framework.Core.Logging.WithEvent
{
    public class EventLogging : IEverythingLogger
    {
        private readonly IEventBus _eventBus;

        public EventLogging(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void LogTrace(string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Trace, message, args));
        }

        public void LogTrace(Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Trace, exception, message, args));
        }

        public void LogTrace(LogEventId eventId, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Trace, eventId, message, args));
        }

        public void LogTrace(LogEventId eventId, Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Trace, eventId, exception, message, args));
        }

        public void LogDebug(string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Debug, message, args));
        }

        public void LogDebug(Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Debug, exception, message, args));
        }

        public void LogDebug(LogEventId eventId, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Debug, eventId, message, args));
        }

        public void LogDebug(LogEventId eventId, Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Debug, eventId, exception, message, args));
        }

        public void LogInformation(string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Information, message, args));
        }

        public void LogInformation(Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Information, exception, message, args));
        }

        public void LogInformation(LogEventId eventId, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Information, eventId, message, args));
        }

        public void LogInformation(LogEventId eventId, Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Information, eventId, exception, message, args));
        }

        public void LogWarning(string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Warning, message, args));
        }

        public void LogWarning(Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Warning, exception, message, args));
        }

        public void LogWarning(LogEventId eventId, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Warning, eventId, message, args));
        }

        public void LogWarning(LogEventId eventId, Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Warning, eventId, exception, message, args));
        }

        public void LogError(string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Error, message, args));
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Error, exception, message, args));
        }

        public void LogError(LogEventId eventId, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Error, eventId, message, args));
        }

        public void LogError(LogEventId eventId, Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Error, eventId, exception, message, args));
        }

        public void LogCritical(string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Critical, message, args));
        }

        public void LogCritical(Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Critical, exception, message, args));
        }

        public void LogCritical(LogEventId eventId, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Critical, eventId, message, args));
        }

        public void LogCritical(LogEventId eventId, Exception exception, string message, params object[] args)
        {
            _eventBus.Publish(new EventHappened(EventType.Critical, eventId, exception, message, args));
        }
    }
}
