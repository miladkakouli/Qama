using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using Qama.Framework.Core.Abstractions.DAL;

namespace Qama.Framework.Core.Persistence.NHibernate.Listeners
{
    public class PreUpdateEventListener : IPreUpdateEventListener
    {
        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnPreUpdate(@event), cancellationToken);
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var entity = @event.Entity as IHaveOnUpdate;
            Action<string, object> setAction = (string propertyName, object value) =>
            {
                Set(@event.Persister, @event.State, propertyName, value);
            };
            entity?.OnPreUpdate(setAction);
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
