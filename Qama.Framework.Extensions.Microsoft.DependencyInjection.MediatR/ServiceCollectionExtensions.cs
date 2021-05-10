using Qama.Framework.Core.Abstractions.Commands;
using Qama.Framework.Core.Abstractions.Commands.Decorators;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Events.Decorator;
using Qama.Framework.Core.Abstractions.Queries;
using Qama.Framework.Core.Abstractions.Queries.Decorators;
using Qama.Framework.Core.Abstractions.Validator;
using Qama.Framework.Core.CommandBus.MediatR;
using Qama.Framework.Core.EventBus.MediatR;
using Qama.Framework.Core.QueryBus.MediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection.MediatR
{

    public static class ServiceCollectionExtensions
    {
        public static void AddMediatRServiceBus(this IServiceCollection services, params Type[] type)
        {
            services.AddMediatR(type);
        }
        public static void AddMediatRQueryHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, IQueryHandler<T>
            where T : QueryBase
        {
            services.AddScoped<T2>();
            services.AddScoped<IRequestHandler<MediatRQueryBase<T, IQueryResult>, IQueryResult>, MediatRRequestQueryHandler<T>>();
            services.AddScoped<IQueryHandler<T>>(x =>
                new ValidationalQueryHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>()));
        }

        public static void AddMediatRCommandHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, ICommandHandler<T>
            where T : CommandBase
        {
            services.AddScoped<T2>();
            services.AddScoped<IRequestHandler<MediatRCommandBase<T>, Unit>, MediatRRequestCommandHandler<T>>();
            services.AddScoped<ICommandHandler<T>>(x =>
                new ValidationalCommandHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>()));
        }

        public static void AddMediatREventHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, IEventHandler<T>
            where T : EventBase
        {
            services.AddScoped<T2>();
            services.AddScoped<INotificationHandler<MediatREventBase<T>>, MediatRRequestEventHandler<T>>();
            services.AddScoped<IEventHandler<T>>(x =>
                new ValidationalEventHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>()));
        }

        public static void AddMediatREventHandlers<T>(this IServiceCollection services, params Type[] types)
            where T : EventBase
        {
            services.AddScoped<INotificationHandler<MediatREventBase<T>>, MediatRRequestEventHandler<T>>();
            foreach (var type in types)
            {
                services.AddScoped(type);
                services.AddScoped(typeof(IEventHandler<T>), type);
            }
        }

        //public static void AddMediatREventHandlerWithValidationDecorator<T>(this IServiceCollection services, params Type[] types)
        //    where T : EventBase
        //{
        //    services.AddScoped<INotificationHandler<MediatREventBase<T>>, MediatRRequestEventHandler<T>>();
        //    foreach (var type in types)
        //    {
        //        services.AddScoped(type);
        //        services.AddScoped<IEventHandler<T>>(x =>
        //            new ValidationalEventHandlerDecorator<T>(x.GetService(type), x.GetServices<IValidator<T>>()));
        //    }

        //}
    }
}
