using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.ServiceLocator;
using Qama.Framework.Extensions.Serializer;
using RabbitMQ.Client;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly RabbitMQSettings _rabbitMqOptions;
        private readonly IModel _channel;
        private readonly IServiceLocator _serviceLocator;
        private readonly IEverythingLogger _everythingLogger;
        private static readonly List<object> _eventHandlers;

        public RabbitMQEventBus(IModel channel, RabbitMQSettings rabbitMqOptions,
            IServiceLocator serviceLocator, IEverythingLogger everythingLogger)
        {
            _channel = channel;
            _rabbitMqOptions = rabbitMqOptions;
            _serviceLocator = serviceLocator;
            _everythingLogger = everythingLogger;
        }
        static RabbitMQEventBus()
        {
            _eventHandlers = new List<object>();
        }
        public Task Publish<T>(T @event)
            where T : EventBase
        {
            _channel.ExchangeDeclare(
                exchange: _rabbitMqOptions.Exchange,
                type: _rabbitMqOptions.ConnectionType);
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            _everythingLogger.LogDebug($"Publishing an event to {_rabbitMqOptions.Exchange} and Queue " +
                                       $"{@event.GetRoutingKey()}");
            _channel.BasicPublish(exchange: _rabbitMqOptions.Exchange,
                routingKey: @event.GetRoutingKey(),
                basicProperties: properties,
                body: (@event).ToJsonByteArray());
            return Task.CompletedTask;
        }

        public void Subscribe<T, TEventHandler>()
            where T : EventBase
            where TEventHandler : IEventHandler<T>
        {
            var @event = (T)Activator.CreateInstance(typeof(T));
            _channel.ExchangeDeclare(exchange: _rabbitMqOptions.Exchange, type: _rabbitMqOptions.ConnectionType);
            //var @event = new RabbitMQEventBase<T>();
            IDictionary<string, object> args = new Dictionary<string, object>();
            args.Add("x-max-priority ", 10);

            _everythingLogger.LogDebug($"Subscribing TestEvent to {_rabbitMqOptions.Exchange} and Queue " +
                                       $"{@event.GetRoutingKey()}");

            var queueName = _channel.QueueDeclare(queue: @event.GetRoutingKey(),
                durable: true, exclusive: false, autoDelete: false, arguments: args).QueueName;

            _channel.QueueBind(queue: queueName,
                    exchange: _rabbitMqOptions.Exchange,
                    routingKey: @event.GetRoutingKey());

            _channel.BasicQos(0, 1, false);
            var consumer = new RabbitMQEventHandler<T>(_channel, _serviceLocator);
            _eventHandlers.Add(consumer);
            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void Subscribe<T>(Type type)
            where T : EventBase
        {
            _channel.ExchangeDeclare(exchange: _rabbitMqOptions.Exchange, type: _rabbitMqOptions.ConnectionType);
            var @event = (T)Activator.CreateInstance(typeof(T));
            IDictionary<string, object> args = new Dictionary<string, object>();
            args.Add("x-max-priority ", 10);

            var queueName = _channel.QueueDeclare(queue: @event.GetRoutingKey(),
                durable: true, exclusive: false, autoDelete: false, arguments: args).QueueName;

            _channel.QueueBind(queue: queueName,
                exchange: _rabbitMqOptions.Exchange,
                routingKey: @event.GetRoutingKey());

            _channel.BasicQos(0, 1, false);
            var consumer = new RabbitMQEventHandler<T>(_channel, _serviceLocator);
            _eventHandlers.Add(consumer);
            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

    }
}