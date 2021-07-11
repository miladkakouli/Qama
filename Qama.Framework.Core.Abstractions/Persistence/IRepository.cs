using System;
using System.Collections.Generic;
using Qama.Framework.Core.Abstractions.DAL;

namespace Qama.Framework.Core.Abstractions.Persistence
{
    public interface IRepository<T, TKey>
        where T : AggregateRoot<TKey>
    {
        void Add(T aggregateRoot);
        void Update(T aggregateRoot);
        void Delete(T aggregateRoot);
        T GetById(TKey id);
        T GetBy(Func<T, bool> predicate);
        bool HasId(TKey id);
        bool Any(Func<T, bool> predicate);
        bool All(Func<T, bool> predicate);
        void BulkInsert<TEntity, TKeyEntity>(IEnumerable<TEntity> entities) where TEntity : Entity<TKeyEntity>;
    }
}