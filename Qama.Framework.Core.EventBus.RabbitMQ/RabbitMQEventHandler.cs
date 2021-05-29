using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.ServiceLocator;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Qama.Framework.Extensions.Serializer;


namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQEventHandler<T> : EventingBasicConsumer
        where T : EventBase
    {
        private readonly IServiceLocator _serviceLocator;

        public RabbitMQEventHandler(IModel model, IServiceLocator serviceLocator)
            : base(model)
        {
            _serviceLocator = serviceLocator;
            this.Received += async (sender, args) =>
            {
                try
                {
                    using (var scope = _serviceLocator.GetInstance<IServiceProvider>().CreateScope())
                    {
                        await scope.ServiceProvider.GetService<IEventHandler<T>>()
                            .Handle(Encoding.UTF8.GetString(args.Body.ToArray()).FromJsonString<T>());
                    }
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
