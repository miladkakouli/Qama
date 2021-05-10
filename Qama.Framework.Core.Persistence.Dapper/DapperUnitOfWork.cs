using System.Data;
using Qama.Framework.Core.Abstractions.Persistence;

namespace Qama.Framework.Core.Persistence.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        public DapperUnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public void Dispose() => _dbConnection.Dispose();

        public void Begin() => _dbTransaction = _dbConnection.BeginTransaction();

        public void Commit() => _dbTransaction.Commit();

        public void Rollback() => _dbTransaction.Rollback();

        public string GetDatabaseName() => _dbConnection.Database;

        public string GetConnectionString() => _dbConnection.ConnectionString;
    }
}
