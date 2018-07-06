using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public static class ServiceCollectionExtensions
    {
        public static TenantIdentification AddTenantIdentification(this IServiceCollection services)
        {
            return new TenantIdentification(services);
;        }

        public static IServiceCollection AddTenantScriptAndStyle(this IServiceCollection services, bool injectScript = true, bool injectStylesheet = true, string rootPath = null)
        {
            services.AddScoped<ITagHelperComponent, TenantTagHelperComponent>(sp => new TenantTagHelperComponent(sp.GetRequiredService<ITenantService>(), injectScript, injectStylesheet, rootPath));
            return services;
        }

        public static IServiceCollection AddTenantService(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            return services;
        }

        public static IServiceCollection AddQueryStringIdentificationService(this IServiceCollection services, string tenantKey = "Tenant")
        {
            services.AddSingleton<ITenantIdentificationService, QueryStringTenantIdentificationService>(sp => new QueryStringTenantIdentificationService(sp.GetRequiredService<IConfiguration>(), tenantKey));
            return services;
        }

        public static IServiceCollection AddHostIdentificationService(this IServiceCollection services)
        {
            services.AddSingleton<ITenantIdentificationService, HostTenantIdentificationService>();
            return services;
        }

        public static DbContextIdentification AddTenantDbContextIdentitication(this IServiceCollection services)
        {
            return new DbContextIdentification(services);
        }
    }
}
