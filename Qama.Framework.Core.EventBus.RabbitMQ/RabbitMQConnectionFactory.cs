using RabbitMQ.Client;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public static class RabbitMQConnectionFactory
    {
        public static IConnection Create(RabbitMQSettings settings)
        {
            return new ConnectionFactory()
            {
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password
            }.CreateConnection();
        }
    }
}
