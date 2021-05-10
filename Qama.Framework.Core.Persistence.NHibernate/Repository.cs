using System.Data;
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
        private readonly ISession _session;
        private readonly IEventBus _eventBus;
        private readonly IEverythingLogger _everythingLogger;
        private readonly IServiceLocator _serviceLocator;

        public Repository(ISession session, IEventBus eventBus, IEverythingLogger everythingLogger, IServiceLocator serviceLocator)
        {
            _session = session;
            _eventBus = eventBus;
            _everythingLogger = everythingLogger;
            _serviceLocator = serviceLocator;
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

        public T GetById<TIdType>(Id<TIdType> id)
        {
            return _session.Get<T>(id);
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
