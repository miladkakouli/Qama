using RabbitMQ.Client;

namespace Qama.Framework.Core.EventBus.RabbitMQ.Abstractions
{
    public interface IChannelProvider
    {
        IModel GenerateChannel();
    }
}
