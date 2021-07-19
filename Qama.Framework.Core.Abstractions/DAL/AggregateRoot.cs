using System.Collections.Generic;
using System.Dynamic;
using Qama.Framework.Core.Abstractions.Events;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
        public AggregateRoot()
        {
            this.Events = this.Events ?? (IList<EventBase>)new List<EventBase>();
        }
        public virtual IList<EventBase> Events { get; protected set; }

        public virtual void AddEvent(EventBase @event)
        {
            Events ??= new List<EventBase>();
            Events.Add(@event);
        }

        public virtual void RemoveEvent(EventBase @event)
        {
            Events?.Remove(@event);
        }

        public virtual void ClearEvents()
        {
            Events?.Clear();
        }
    }
}
