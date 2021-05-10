using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Events
{
    public interface IEventHandler<T> where T : EventBase
    {
        Task Handle(T @event);
    }
}
