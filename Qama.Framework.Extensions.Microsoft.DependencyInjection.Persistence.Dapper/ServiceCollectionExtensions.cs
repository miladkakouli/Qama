using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Persistence;
using Qama.Framework.Core.Abstractions.Settings;
using Qama.Framework.Core.Persistence.Dapper;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection.Persistence.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDapperPersistence<T>(this IServiceCollection services)
            where T : DbConnectionString
        {
            services.AddSingleton<T>(x => x.GetService<ISetting<T>>().GetValue());
            services.AddSingleton<IDbConnection>(x => new SqlConnection(x.GetService<T>().ConnectionString));
        }
    }
}
