using System;
using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Events
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : EventBase;
        void Subscribe<T, TEventHandler>()
            where T : EventBase
            where TEventHandler : IEventHandler<T>;

        void Subscribe<T>(Type type)
            where T : EventBase;
    }
}
