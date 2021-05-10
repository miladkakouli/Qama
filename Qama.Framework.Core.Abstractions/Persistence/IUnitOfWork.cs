using System;

namespace Qama.Framework.Core.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        string GetDatabaseName();
        string GetConnectionString();
    }
}
