using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public static class TenantIdentificationExtensions
    {
        public static IServiceCollection TenantForHost(this TenantIdentification identification)
        {
            return identification._services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>();
        }

        public static IServiceCollection TenantForQueryString(this TenantIdentification identification)
        {
            return identification._services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>();
        }
    }
}
