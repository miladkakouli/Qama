using System;
using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Commands;
using Qama.Framework.Core.Abstractions.Commands.Decorators;
using Qama.Framework.Core.Abstractions.DAL;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Events.Decorator;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.Persistence;
using Qama.Framework.Core.Abstractions.Queries;
using Qama.Framework.Core.Abstractions.Queries.Decorators;
using Qama.Framework.Core.Abstractions.ServiceLocator;
using Qama.Framework.Core.Abstractions.Settings;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceLocator<T>(this IServiceCollection services) where T : class, IServiceLocator
        {
            services.AddTransient(typeof(IServiceLocator), typeof(T));
        }

        public static void AddCommandBus<T>(this IServiceCollection services) where T : class, ICommandBus
        {
            services.AddScoped(typeof(ICommandBus), typeof(T));
        }

        public static void AddQueryBus<T>(this IServiceCollection services) where T : class, IQueryBus
        {
            services.AddScoped(typeof(IQueryBus), typeof(T));
        }

        public static void AddLogger<T>(this IServiceCollection services) where T : class, IEverythingLogger
        {
            services.AddScoped(typeof(IEverythingLogger), typeof(T));
        }
        public static void AddEventBus<T>(this IServiceCollection services) where T : class, IEventBus
        {
            services.AddScoped(typeof(IEventBus), typeof(T));
        }

        public static void AddValidator<T, T2>(this IServiceCollection services)
            where T : IValidatable
            where T2 : class, IValidator<T>
        {
            services.AddScoped(typeof(IValidator<T>), typeof(T2));
        }

        public static void AddValidators<T>(this IServiceCollection services, params Type[] types)
            where T : IValidatable
        {
            foreach (var type in types)
            {
                services.AddScoped(typeof(IValidator<T>), type);
            }
        }

        public static void AddCommandHandler<T, T2>(this IServiceCollection services)
            where T2 : class, ICommandHandler<T>
            where T : CommandBase
        {
            services.AddScoped<ICommandHandler<T>, T2>();
        }

        public static void AddCommandHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, ICommandHandler<T>
            where T : CommandBase
        {
            services.AddScoped<T2>();
            services.AddScoped<ICommandHandler<T>>(x =>
                new ValidationalCommandHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>()));
        }

        public static void AddCommandHandlerWithTransactionalDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, ICommandHandler<T>
            where T : CommandBase
        {
            services.AddScoped<T2>();
            services.AddScoped<ICommandHandler<T>>(x =>
                new TransactionalCommandHandlerDecorator<T>(x.GetService<T2>(), x.GetService<IUnitOfWork>()));
        }

        public static void AddCommandHandlerWithValidationalAndTransactionalDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, ICommandHandler<T>
            where T : CommandBase
        {
            services.AddScoped<T2>();
            services.AddScoped<ICommandHandler<T>>(x =>
                new ValidationalCommandHandlerDecorator<T>(new TransactionalCommandHandlerDecorator<T>(x.GetService<T2>(), x.GetService<IUnitOfWork>())
                    , x.GetServices<IValidator<T>>()));
        }

        public static void AddQueryHandler<T, T2>(this IServiceCollection services)
            where T2 : class, IQueryHandler<T>
            where T : QueryBase
        {
            services.AddScoped<IQueryHandler<T>, T2>();
        }

        public static void AddQueryHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, IQueryHandler<T>
            where T : QueryBase
        {
            services.AddScoped<T2>();
            services.AddScoped<IQueryHandler<T>>(x =>
                new ValidationalQueryHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>()));
        }


        public static void AddEventHandler<T, T2>(this IServiceCollection services)
            where T2 : class, IEventHandler<T>
            where T : EventBase
        {
            services.AddScoped<IEventHandler<T>, T2>();
        }

        public static void AddEventHandlers<T>(this IServiceCollection services, params Type[] types)
            where T : EventBase
        {
            foreach (var type in types)
            {
                services.AddScoped(typeof(IEventHandler<T>), type);
            }
        }

        public static void AddEventHandlerWithValidationDecorator<T, T2>(this IServiceCollection services)
            where T2 : class, IEventHandler<T>
            where T : EventBase
        {
            services.AddScoped<T2>();
            services.AddScoped<IEventHandler<T>>(x =>
                new ValidationalEventHandlerDecorator<T>(x.GetService<T2>(), x.GetServices<IValidator<T>>()));
        }

        public static void AddUnitOfWork<T>(this IServiceCollection services)
            where T : class, IUnitOfWork
        {
            services.AddScoped<IUnitOfWork, T>();
        }

        public static void AddRepository<T, TKey, T2>(this IServiceCollection services)
            where T : AggregateRoot<TKey>
            where T2 : class, IRepository<T, TKey>

        {
            services.AddScoped<IRepository<T, TKey>, T2>();
        }

    }
}
