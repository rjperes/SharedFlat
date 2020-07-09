using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public static class RazorPageExtensions
    {
        public static bool IsTenant(this IRazorPage page, string tenant)
        {
            var service = page.ViewContext.HttpContext.RequestServices.GetService<ITenantService>();

            return tenant == service.GetCurrentTenant();
        }

        public static bool IsEnabledForTenant(this IRazorPage page, string setting, bool defaultValue = false)
        {
            var service = page.ViewContext.HttpContext.RequestServices.GetService<ITenantService>();
            var tenant = service.GetCurrentTenant();
            var configuration = page.ViewContext.HttpContext.RequestServices.GetService<IConfiguration>();
            var section = configuration.GetSection(nameof(ConfigurationExtensions.Tenants)).GetSection(tenant);

            if (section.Exists())
            {
                return section.GetValue<bool>(setting, defaultValue);
            }
            else
            {
                return configuration.GetSection(nameof(ConfigurationExtensions.Tenants)).GetValue<bool>(setting, defaultValue);
            }
        }

        public static T GetValueForTenant<T>(this IRazorPage page, string setting, T defaultValue = default(T))
        {
            var service = page.ViewContext.HttpContext.RequestServices.GetService<ITenantService>();
            var tenant = service.GetCurrentTenant();
            var configuration = page.ViewContext.HttpContext.RequestServices.GetService<IConfiguration>();
            var section = configuration.GetSection(nameof(ConfigurationExtensions.Tenants)).GetSection(tenant);

            if (section.Exists())
            {
                return section.GetValue<T>(setting, defaultValue);
            }
            else
            {
                return configuration.GetValue<T>(setting, defaultValue);
            }
        }
    }
}
