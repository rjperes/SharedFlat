using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedFlat.Services;
using System;

namespace SharedFlat.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T GetTenantConfiguration<T>(this IServiceProvider serviceProvider) where T : new()
        {
            ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
            var tenant = serviceProvider.GetRequiredService<ITenantService>().GetCurrentTenant();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var obj = new T();
            configuration.Bind($"{nameof(ConfigurationExtensions.Tenants)}:Configuration:{tenant}", obj);
            return obj;
        }
    }
}
