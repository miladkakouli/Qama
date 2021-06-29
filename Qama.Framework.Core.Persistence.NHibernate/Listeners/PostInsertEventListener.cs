using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using Qama.Framework.Core.Abstractions.DAL;

namespace Qama.Framework.Core.Persistence.NHibernate.Listeners
{
    public class PostInsertEventListener : IPostInsertEventListener
    {
        public Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnPostInsert(@event), cancellationToken);
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            var entity = @event.Entity as IHaveOnCreate;
            entity?.OnPostCreate();
        }
    }
}
