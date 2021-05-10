using System.Data;
using Dapper.Contrib.Extensions;
using Qama.Framework.Core.Abstractions.DAL;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.Persistence;

namespace Qama.Framework.Core.Persistence.Dapper
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : AggregateRoot<TKey>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IEventBus _eventBus;
        private readonly IEverythingLogger _everythingLogger;

        public Repository(IDbConnection dbConnection, IEventBus eventBus, IEverythingLogger everythingLogger)
        {
            _dbConnection = dbConnection;
            _eventBus = eventBus;
            _everythingLogger = everythingLogger;
        }
        public void Add(T aggregateRoot)
        {
            _everythingLogger.LogDebug($"adding {aggregateRoot}");
            _dbConnection.Insert(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        public void Delete(T aggregateRoot)
        {
            _dbConnection.Delete(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        public T GetById<TIdType>(Id<TIdType> id)
        {
            return _dbConnection.Get<T>(id);
        }

        public void Update(T aggregateRoot)
        {
            _dbConnection.Update(aggregateRoot);
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
