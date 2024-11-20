using Microsoft.Extensions.Configuration;

namespace SharedFlat.Extensions
{
    public static class ConfigurationExtensions
    {
        public static readonly string Tenants = nameof(Tenants);

        public static TenantMapping GetTenantMapping(this IConfiguration configuration)
        {
            return configuration.GetSection(nameof(Tenants)).Get<TenantMapping>();
        }

        public static T GetTenantConfiguration<T>(this IConfiguration configuration, string tenant) where T : new()
        {
            return configuration.GetValue<T>($"{nameof(Tenants)}:Configuration:{tenant}");
        }

        public static T BindTenantConfiguration<T>(this IConfiguration configuration, string tenant, T instance)
        {
            configuration.GetSection($"{nameof(Tenants)}:Configuration:{tenant}").Bind(instance);
            return instance;
        }

        public static TenantMapping BindTenantsMapping(this IConfiguration configuration, TenantMapping mapping)
        {
            configuration.GetSection($"{nameof(Tenants)}").Bind(mapping);
            return mapping;
        }
    }
}
