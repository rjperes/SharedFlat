using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public static class RazorPageExtensions
    {
        public static bool IsEnabled(this IRazorPage page, string setting, bool defaultValue)
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
                return configuration.GetValue<bool>(setting, defaultValue);
            }
        }

        public static T GetValue<T>(this IRazorPage page, string setting)
        {
            var service = page.ViewContext.HttpContext.RequestServices.GetService<ITenantService>();
            var tenant = service.GetCurrentTenant();
            var configuration = page.ViewContext.HttpContext.RequestServices.GetService<IConfiguration>();
            var section = configuration.GetSection(nameof(ConfigurationExtensions.Tenants)).GetSection(tenant);

            if (section.Exists())
            {
                return section.GetValue<T>(setting);
            }
            else
            {
                return configuration.GetValue<T>(setting);
            }
        }
    }
}
