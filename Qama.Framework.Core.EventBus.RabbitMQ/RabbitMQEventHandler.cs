using System.IO;
using Qama.Framework.Core.Abstractions.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Qama.Framework.Extensions.Serializer;


namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQEventHandler<T> : EventingBasicConsumer
        where T : EventBase
    {
        private readonly IEventHandler<T> _eventHandler;

        public RabbitMQEventHandler(IModel model, IEventHandler<T> eventHandler)
            : base(model)
        {
            _eventHandler = eventHandler;
            this.Received += (sender, args) =>
            {
                try
                {
                    _eventHandler.Handle(args.Body.ToArray().FromJsonByteArray<T>());
                }
                catch
                {
                    model.BasicNack(args.DeliveryTag, false, true);
                    return;
                }
                model.BasicAck(args.DeliveryTag, false);
            };

        }
    }
}
