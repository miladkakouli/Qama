using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        T2 GetBy<T2>(Expression<Func<T2, bool>> predicate) where T2 : class;
        bool HasId(TKey id);
        bool Any(Expression<Func<T, bool>> predicate);
        bool All(Expression<Func<T, bool>> predicate);
        void BulkInsert<TEntity, TKeyEntity>(IEnumerable<TEntity> entities) where TEntity : Entity<TKeyEntity>;
    }
}