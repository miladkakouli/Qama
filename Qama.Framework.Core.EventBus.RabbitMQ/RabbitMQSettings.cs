using Qama.Framework.Core.Abstractions.Settings;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQSettings : ISettingValue
    {
        public string HostName { get; set; }
        public string Exchange { get; set; }
        public string ConnectionType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
