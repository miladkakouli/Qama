using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.Settings;
using Qama.Framework.Core.Settings.Microsoft;

namespace Qama.Framework.Extensions.Microsoft.DependencyInjection.MicrosoftSettingProvider
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMicrosoftSettingProvider<T>(this IServiceCollection services, string name, IConfiguration configuration)
            where T : class, ISettingValue

        {
            services.AddOptions<T>().Bind(configuration.GetSection(name));
            services.AddSingleton<ISetting<T>, MicrosoftSetting<T>>();
        }
    }
}
