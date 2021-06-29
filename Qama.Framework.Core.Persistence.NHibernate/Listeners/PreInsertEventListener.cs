using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NHibernate.Persister.Collection;
using NHibernate.Persister.Entity;
using Qama.Framework.Core.Abstractions.DAL;

namespace Qama.Framework.Core.Persistence.NHibernate.Listeners
{
    public class PreInsertEventListener : IPreInsertEventListener
    {
        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnPreInsert(@event), cancellationToken);
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var entity = @event.Entity as IHaveOnCreate;
            Action<string, object> setAction = (string propertyName, object value) =>
             {
                 Set(@event.Persister, @event.State, propertyName, value);
             };
            entity?.OnPreCreate(setAction);
            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}
