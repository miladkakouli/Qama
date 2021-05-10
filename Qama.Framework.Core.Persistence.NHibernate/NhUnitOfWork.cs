using System;
using Qama.Framework.Core.Abstractions.Persistence;
using NHibernate;

namespace Qama.Framework.Core.Persistence.NHibernate
{
    public class NhUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        public NhUnitOfWork(ISession session)
        {
            this._session = session;
        }
        public void Dispose()
        {
            _session.Dispose();
        }

        public void Begin()
        {
            _session.BeginTransaction();
        }

        public void Commit()
        {
            _session.GetCurrentTransaction().Commit();
        }

        public void Rollback()
        {
            _session.GetCurrentTransaction().Rollback();
        }

        public string GetDatabaseName()
        {
            return _session.Connection.Database;
        }

        public string GetConnectionString()
        {
            return _session.Connection.ConnectionString;
        }
    }
}
