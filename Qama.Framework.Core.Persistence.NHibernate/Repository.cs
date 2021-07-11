using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Persister.Entity;
using NHibernate.Type;
using Qama.Framework.Core.Abstractions.DAL;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.Persistence;
using Qama.Framework.Core.Abstractions.ServiceLocator;

namespace Qama.Framework.Core.Persistence.NHibernate
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : AggregateRoot<TKey>
    {
        protected readonly ISession _session;
        protected readonly IEventBus _eventBus;
        protected readonly IEverythingLogger _everythingLogger;

        public Repository(ISession session, IEventBus eventBus, IEverythingLogger everythingLogger)
        {
            _session = session;
            _eventBus = eventBus;
            _everythingLogger = everythingLogger;
        }
        public void Add(T aggregateRoot)
        {
            _everythingLogger.LogDebug($"adding {aggregateRoot}");
            _session.Save(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        public void Delete(T aggregateRoot)
        {
            _session.Delete(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        public T GetById(TKey id)
        {
            return _session.Get<T>(id);
        }
        public T GetBy(Func<T, bool> predicate)
        {
            return _session.Query<T>().FirstOrDefault(predicate);
        }
        public bool HasId(TKey id)
        {
            return _session.Query<T>().Any(x => x.Id.Equals(id));
        }

        public bool Any(Func<T, bool> predicate)
        {
            return _session.Query<T>().Any(predicate);
        }

        public bool All(Func<T, bool> predicate)
        {
            return _session.Query<T>().All(predicate);
        }

        public void Update(T aggregateRoot)
        {
            _session.Update(aggregateRoot);
            PublishEvents(aggregateRoot);
        }

        private void PublishEvents(T aggregateRoot)
        {
            _everythingLogger.LogDebug($"Publishing {aggregateRoot.Events.Count} events");
            foreach (var @event in aggregateRoot.Events)
            {
                _everythingLogger.LogDebug($"Publishing {@event} event");
                _eventBus.Publish(@event);
            }
            aggregateRoot.ClearEvents();
        }
        public static T Cast<T>(object o)
        {
            return ((Entity<T>)o).Id;
        }
        private DataTable GenerateDataTable<TEntity, TKeyEntity>(IEnumerable<TEntity> entities, SingleTableEntityPersister classMapping) where TEntity : Entity<TKeyEntity>
        {
            var generator = classMapping.IdentifierGenerator;

            var entityTable = new DataTable();
            var propertyNames = classMapping.PropertyNames;

            //var identifierColumnNames = classMapping.IdentifierColumnNames.FirstOrDefault();
            //if (identifierColumnNames != null)
            //{
            //    entityTable.Columns.Add(identifierColumnNames, typeof(TKey));
            //}

            foreach (var identifier in classMapping.IdentifierColumnNames)
            {
                if (classMapping.IdentifierType.IsComponentType)
                {
                    int index = Array.IndexOf(classMapping.IdentifierColumnNames, identifier);
                    entityTable.Columns.Add(identifier,
                        ((ComponentType)classMapping.IdentifierType).Subtypes[index].ReturnedClass);
                }
                else
                    entityTable.Columns.Add(identifier, typeof(TKeyEntity));
            }


            var peristedProperties = propertyNames.Select((propertyName) =>
            {
                var propertyType = classMapping.GetPropertyType(propertyName);
                if (propertyType.IsCollectionType)
                {
                    return null;
                }
                var type = propertyType.ReturnedClass;
                if (propertyType.IsEntityType)
                {
                    Type baseType = propertyType.ReturnedClass;
                    while (baseType.BaseType != null)
                    {
                        baseType = baseType.BaseType;
                        if (baseType.IsGenericType && baseType.Name == typeof(Entity<>).Name)
                            type = baseType.GenericTypeArguments.FirstOrDefault();
                    }
                }
                var columnName = classMapping.GetPropertyColumnNames(propertyName).FirstOrDefault();
                if (columnName == null)
                {
                    return null;
                }
                entityTable.Columns.Add(columnName, type);

                return new
                {
                    ColumnName = columnName,
                    PropertyName = propertyName,
                    IsEnum = propertyType.ReturnedClass.IsEnum,
                    IsEntity = propertyType.IsEntityType,
                    EntityId = type,
                    Type = propertyType.ReturnedClass
                };
            }).Where(x => x != null).ToList();

            foreach (var entity in entities)
            {
                var row = entityTable.NewRow();

                if (classMapping.IdentifierColumnNames != null && generator.GetType().Name != "Assigned")
                {
                    foreach (var identifier in classMapping.IdentifierColumnNames)
                    {
                        var valueIdentifier = (TKeyEntity)generator.Generate(_session.GetSessionImplementation(), null);
                        row[identifier] = valueIdentifier;
                        //entityTable.Columns.Add(identifier, typeof(TKey));
                    }
                }
                else if (classMapping.IdentifierType.IsComponentType)
                {
                    var componentIdentity = (TKeyEntity)classMapping.GetIdentifier(entity);
                    foreach (var identifier in classMapping.IdentifierColumnNames)
                    {
                        PropertyInfo property = componentIdentity.GetType().GetProperty(identifier);
                        row[identifier] = property.GetValue(componentIdentity, null);
                    }
                }

                foreach (var persistedProperty in peristedProperties)
                {
                    var columnName = persistedProperty.ColumnName;
                    if (columnName != null)
                    {
                        object value = classMapping.GetPropertyValue(entity, persistedProperty.PropertyName);

                        if (value == null)
                        {
                            row[columnName] = DBNull.Value;
                        }
                        else
                        {
                            if (persistedProperty.IsEnum)
                            {
                                row[columnName] = Convert.ToInt64(value);
                            }
                            else if (persistedProperty.IsEntity)
                            {
                                MethodInfo castMethod =
                                    this.GetType().GetMethod("Cast")?.MakeGenericMethod(persistedProperty.EntityId) ??
                                    this.GetType().BaseType?.GetMethod("Cast")?.MakeGenericMethod(persistedProperty.EntityId);
                                row[columnName] = castMethod?.Invoke(null, new object[] { value });
                            }
                            else
                            {
                                row[columnName] = value;
                            }
                        }
                    }

                }
                entityTable.Rows.Add(row);
            }
            return entityTable;
        }
        public void BulkInsert<TEntity, TKeyEntity>(IEnumerable<TEntity> entities) where TEntity : Entity<TKeyEntity>
        {
            var allClassMetadata = _session.SessionFactory.GetAllClassMetadata();
            var classMapping = allClassMetadata.Values.Where(x => x is SingleTableEntityPersister)
                .Cast<SingleTableEntityPersister>()
                .FirstOrDefault(x => x.MappedClass.Name == typeof(TEntity).Name);

            using (var insertEntitiesCmd = new SqlCommand())
            {
                if (_session.Transaction != null && _session.Transaction.IsActive)
                {
                    _session.GetCurrentTransaction().Enlist(insertEntitiesCmd);
                }
                var entityTable = GenerateDataTable<TEntity, TKeyEntity>(entities, classMapping);
                var insertBulk = new SqlBulkCopy((SqlConnection)_session.Connection,
                        SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity,
                        insertEntitiesCmd.Transaction)
                { DestinationTableName = classMapping.TableName };

                insertBulk.BulkCopyTimeout = 0;
                foreach (DataColumn column in entityTable.Columns)
                {
                    insertBulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                _session.Flush();
                insertBulk.WriteToServer(entityTable);
            }
        }
    }
}
