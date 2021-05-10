using System;
using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Events.Decorator;
using Qama.Framework.Core.Abstractions.Settings;
using Qama.Framework.Core.Abstractions.Validator;
using Qama.Framework.Core.EventBus.RabbitMQ;
using Qama.Framework.Core.EventBus.RabbitMQ.Abstractions;
using RabbitMQ.Client;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitMq<T>(this IServiceCollection services)
            where T : RabbitMQSettings
        {
            services.AddSingleton<T>(x => x.GetService<ISetting<T>>().GetValue());
            services.AddSingleton<IConnection>(x => RabbitMQConnectionFactory.Create(x.GetService<ISetting<T>>().GetValue()));
            services.AddSingleton<IChannelProvider, ChannelProvider>();
            services.AddScoped<IModel>(x => x.GetService<IChannelProvider>().GenerateChannel());
        }

        public static void AddRabbitMQEventHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, IEventHandler<T>
            where T : EventBase
        {
            services.AddScoped<T2>();
            services.AddScoped<IEventHandler<T>>(x =>
            {
                x.GetService<IEventBus>().Subscribe<T, T2>();
                return new ValidationalEventHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>());
            });
        }
        public static void AddRabbitMQEventHandler<T, T2>(this IServiceCollection services)
            where T2 : class, IEventHandler<T>
            where T : EventBase
        {
            services.AddScoped<T2>();
            services.AddScoped<IEventHandler<T>>(x =>
             {
                 x.GetService<IEventBus>().Subscribe<T, T2>();
                 return x.GetService<T2>();
             });
        }

        public static void AddRabbitMQEventHandlers<T>(this IServiceCollection services, params Type[] types)
            where T : EventBase
        {
            foreach (var type in types)
            {
                services.AddScoped(type);
                services.AddScoped(typeof(IEventHandler<T>), x =>
                {
                    x.GetService<IEventBus>().Subscribe<T>(type);
                    return x.GetService(type);
                });
            }
        }

        public static void AddRabbitMQEventHandlersWithValidationDecorator<T>(this IServiceCollection services, params Type[] types)
            where T : EventBase
        {
            foreach (var type in types)
            {
                services.AddScoped(type);
                services.AddScoped(typeof(IEventHandler<T>), x =>
                {
                    x.GetService<IEventBus>().Subscribe<T>(type);
                    return new ValidationalEventHandlerDecorator<T>((IEventHandler<T>)x.GetService(type),
                        x.GetServices<IValidator<T>>());
                });
            }
        }
    }
}
