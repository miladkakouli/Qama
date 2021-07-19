using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
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

        public T GetById(TKey id)
        {
            return _dbConnection.Get<T>(id);
        }

        public T2 GetBy<T2>(Expression<Func<T2, bool>> predicate) where T2 : class
        {
            throw new NotImplementedException();
        }

        public bool HasId(TKey id)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool All(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert<TEntity, TKeyEntity>(IEnumerable<TEntity> entities) where TEntity : Entity<TKeyEntity>
        {
            throw new NotImplementedException();
        }

        public bool HasBy(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
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
