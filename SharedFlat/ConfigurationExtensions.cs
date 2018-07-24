using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace SharedFlat
{
    public static class ConfigurationExtensions
    {
        public static readonly string Tenants = nameof(Tenants);

        public static TenantMapping GetTenantMapping(this IConfiguration configuration)
        {
            return configuration.GetSection(nameof(Tenants)).Get<TenantMapping>();
        }
    }
}
