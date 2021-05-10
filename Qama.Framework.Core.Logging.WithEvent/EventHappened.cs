using System;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;

namespace Qama.Framework.Core.Logging.WithEvent
{
    public class EventHappened : EventBase
    {
        class NullEventId : LogEventId
        {
            public override int GetHashCode() => -1;

            public NullEventId() :
                base(-1, nameof(NullEventId))
            { }
        }
        public EventType EventType { get; private set; }
        public string Message { get; private set; }
        public object[] Args { get; private set; }
        public Exception Exception { get; private set; }
        public LogEventId EventId { get; private set; }
        public EventHappened(EventType eventType, string message, params object[] args) : base()
        {
            EventType = eventType;
            Message = message;
            Args = args;
            EventId = new NullEventId();
        }

        public EventHappened(EventType eventType, Exception exception, string message, params object[] args) : base()
        {
            EventType = eventType;
            Exception = exception;
            Message = message;
            Args = args;
            EventId = new NullEventId();
        }

        public EventHappened(EventType eventType, LogEventId eventId, string message, params object[] args) : base()
        {
            EventType = eventType;
            EventId = eventId;
            Message = message;
            Args = args;
        }

        public EventHappened(EventType eventType, LogEventId eventId, Exception exception, string message, params object[] args) : base()
        {
            EventType = eventType;
            EventId = eventId;
            Exception = exception;
            Message = message;
            Args = args;
        }

        public new static string GetRoutingKey() => "EventHappened";

    }
}
