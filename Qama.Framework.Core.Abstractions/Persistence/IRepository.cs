using Qama.Framework.Core.Abstractions.DAL;

namespace Qama.Framework.Core.Abstractions.Persistence
{
    public interface IRepository<T, TKey>
        where T : AggregateRoot<TKey>
    {
        void Add(T aggregateRoot);
        void Update(T aggregateRoot);
        void Delete(T aggregateRoot);
        T GetById<TIdType>(Id<TIdType> id);
    }
}
