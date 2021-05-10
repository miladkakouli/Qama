using Qama.Framework.Core.EventBus.RabbitMQ.Abstractions;
using RabbitMQ.Client;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class ChannelProvider : IChannelProvider
    {
        private readonly IConnection _connection;
        public ChannelProvider(IConnection connection)
        {
            _connection = connection;
        }
        public IModel GenerateChannel()
        {
            return _connection.CreateModel();
        }
    }
}
