using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using Qama.Framework.Core.Abstractions.DAL;

namespace Qama.Framework.Core.Persistence.NHibernate.Listeners
{
    public class PostUpdateEventListener : IPostUpdateEventListener
    {
        public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnPostUpdate(@event), cancellationToken);
        }

        public void OnPostUpdate(PostUpdateEvent @event)
        {
            var entity = @event.Entity as IHaveOnUpdate;
            entity?.OnPostUpdate();
        }
    }
}
