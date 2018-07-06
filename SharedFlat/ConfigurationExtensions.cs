using Microsoft.Extensions.Configuration;

namespace SharedFlat
{
    public static class ConfigurationExtensions
    {
        public static HostTenantSettings GetHostTenantSettings(this IConfiguration configuration)
        {
            return configuration.GetHostTenantSettings();
        }
    }
}
