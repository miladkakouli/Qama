using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Persistence;
using System.Reflection;
using Qama.Framework.Core.Abstractions.Settings;
using NHibernate;
using Qama.Framework.Core.Persistence.NHibernate;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection.Persistence.NHibernate
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNHibernatePersistence<T>(this IServiceCollection services, Assembly assembly)
            where T : DbConnectionString
        {
            services.AddSingleton<T>(x => x.GetService<ISetting<T>>().GetValue());
            services.AddSingleton<ISessionFactory>(x => SessionFactoryBuilder.Create(x.GetService<T>().ConnectionString, assembly));
            services.AddScoped<ISession>(x => x.GetService<ISessionFactory>().OpenSession());
        }
    }
}
