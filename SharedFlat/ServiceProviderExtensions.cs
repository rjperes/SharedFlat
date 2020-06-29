using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SharedFlat
{
    public static class ServiceProviderExtensions
    {
        public static T GetTenantConfiguration<T>(this IServiceProvider serviceProvider) where T : new()
        {
            var tenant = serviceProvider.GetRequiredService<ITenantService>().GetCurrentTenant();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var obj = new T();
            configuration.Bind($"{nameof(ConfigurationExtensions.Tenants)}:Configuration:{tenant}", obj);
            return obj;
        }
    }
}
