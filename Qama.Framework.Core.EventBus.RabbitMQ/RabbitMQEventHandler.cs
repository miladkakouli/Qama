using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Qama.Framework.Extensions.Serializer;


namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQEventHandler<T> : EventingBasicConsumer
        where T : EventBase
    {
        private readonly IEventHandler<T> _eventHandler;

        public RabbitMQEventHandler(IModel model, IEventHandler<T> eventHandler, IEverythingLogger everythingLogger)
            : base(model)
        {
            _eventHandler = eventHandler;
            this.Received += async (sender, args) =>
            {
                try
                {
                    await _eventHandler.Handle(Encoding.UTF8.GetString(args.Body.ToArray()).FromJsonString<T>());
                }
                catch (Exception e)
                {
                    model.BasicNack(args.DeliveryTag, false, true);
                    return;
                }
                model.BasicAck(args.DeliveryTag, false);
            };
        }
    }
}
