using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Events;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Logging.WithEvent;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection.EventLogging
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEventLogging<T>(this IServiceCollection services)
            where T : class, IEverythingLogger
        {
            services.AddScoped<T>();
            services.AddScoped<IEverythingLogger, Core.Logging.WithEvent.EventLogging>();
            services.AddScoped<IEventHandler<EventHappened>>(
                x => new EventHappenedHandler(x.GetService<T>()));
        }
    }
}
