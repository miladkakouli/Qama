using System;
using System.Data;
using System.Linq;
using NHibernate;
using Qama.Framework.Core.Abstractions.DAL;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.Persistence;
using Qama.Framework.Core.Abstractions.ServiceLocator;

namespace Qama.Framework.Core.Persistence.NHibernate
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : AggregateRoot<TKey>
    {
        protected readonly ISession _session;
        protected readonly IEventBus _eventBus;
        protected readonly IEverythingLogger _everythingLogger;

        public Repository(ISession session, IEventBus eventBus, IEverythingLogger everythingLogger)
        {
            _session = session;
            _eventBus = eventBus;
            _everythingLogger = everythingLogger;
        }
        public void Add(T aggregateRoot)
        {
            _everythingLogger.LogDebug($"adding {aggregateRoot}");
            _session.Save(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        public void Delete(T aggregateRoot)
        {
            _session.Delete(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        public T GetById(TKey id)
        {
            return _session.Get<T>(id);
        }

        public bool HasId(TKey id)
        {
            return _session.Query<T>().Any(x => x.Id.Equals(id));
        }

        public bool HasBy(Func<T, bool> predicate)
        {
            return _session.Query<T>().Any(predicate);
        }

        public void Update(T aggregateRoot)
        {
            _session.Update(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        private void PublishEvents(T aggregateRoot)
        {
            _everythingLogger.LogDebug($"Publishing {aggregateRoot.Events.Count} events");
            foreach (var @event in aggregateRoot.Events)
            {
                _everythingLogger.LogDebug($"Publishing {@event} event");
                _eventBus.Publish(@event);
            }
            aggregateRoot.ClearEvents();
        }
    }
}
