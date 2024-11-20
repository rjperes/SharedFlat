using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedFlat.Services;

namespace SharedFlat.Extensions
{
    public static class RazorPageExtensions
    {
        public static string GetTenant(this IRazorPage page)
        {
            var service = page.ViewContext.HttpContext.RequestServices.GetService<ITenantService>();
            return service.GetCurrentTenant();
        }

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
                return section.GetValue(setting, defaultValue);
            }
            else
            {
                return configuration.GetSection(nameof(ConfigurationExtensions.Tenants)).GetValue(setting, defaultValue);
            }
        }

        public static T GetValueForTenant<T>(this IRazorPage page, string setting, T defaultValue = default)
        {
            var service = page.ViewContext.HttpContext.RequestServices.GetService<ITenantService>();
            var tenant = service.GetCurrentTenant();
            var configuration = page.ViewContext.HttpContext.RequestServices.GetService<IConfiguration>();
            var section = configuration.GetSection(nameof(ConfigurationExtensions.Tenants)).GetSection(tenant);

            if (section.Exists())
            {
                return section.GetValue(setting, defaultValue);
            }
            else
            {
                return configuration.GetValue(setting, defaultValue);
            }
        }
    }
}
