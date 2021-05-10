using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Qama.Framework.Core.Persistence.NHibernate
{
    public static class SessionFactoryBuilder
    {
        public static ISessionFactory Create(string connectionString, Assembly mappingAssembly)
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(cfg =>
            {
                cfg.Dialect<MsSql2012Dialect>();
                cfg.Driver<SqlClientDriver>();
                // cfg.ConnectionStringName = connectionStringName;
                cfg.ConnectionString = connectionString;
                cfg.IsolationLevel = IsolationLevel.ReadCommitted;
            });

            var modelMapper = new ModelMapper();
            modelMapper.AddMappings(mappingAssembly.GetExportedTypes());
            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddDeserializedMapping(hbmMapping, "test");
            var session = configuration.BuildSessionFactory();
            return session;
        }
    }
}
