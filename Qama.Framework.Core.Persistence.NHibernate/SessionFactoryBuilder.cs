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
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Qama.Framework.Core.Persistence.NHibernate.Listeners;

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
            var hbmMapping = modelMapper.CompileMappingForEachExplicitlyAddedEntity();
            hbmMapping.WriteAllXmlMapping();
            configuration.AddDeserializedMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities(), "test");
            configuration.AppendListeners(ListenerType.PreUpdate, new IPreUpdateEventListener[]
            {
                new PreUpdateEventListener(),
            });
            configuration.AppendListeners(ListenerType.PostUpdate, new IPostUpdateEventListener[]
            {
                new PostUpdateEventListener(),
            });
            configuration.AppendListeners(ListenerType.PreInsert, new IPreInsertEventListener[]
            {
                new PreInsertEventListener(),
            });
            configuration.AppendListeners(ListenerType.PostInsert, new IPostInsertEventListener[]
            {
                new PostInsertEventListener(),
            });
            var session = configuration.BuildSessionFactory();
            return session;
        }
    }
}
