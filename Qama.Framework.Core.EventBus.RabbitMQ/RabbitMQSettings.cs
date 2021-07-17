using Qama.Framework.Core.Abstractions.Settings;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQSettings : ISettingValue
    {
        public string HostName { get; set; }
        public string PublishExchange { get; set; }
        public string SubscribeExchange { get; set; }
        public string PublishConnectionType { get; set; }
        public string SubscribeConnectionType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
