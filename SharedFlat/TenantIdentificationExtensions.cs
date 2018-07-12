using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public static class TenantIdentificationExtensions
    {
        public static IServiceCollection DynamicTenant(this TenantIdentification identification, Func<HttpContext, string> currentTenant, Func<IEnumerable<string>> allTenants)
        {
            return identification._services.AddScoped<ITenantIdentificationService>(sp => new DynamicTenantIdentificationService(currentTenant, allTenants));
        }

        public static IServiceCollection TenantForHost(this TenantIdentification identification)
        {
            return identification._services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>();
        }

        public static IServiceCollection TenantForQueryString(this TenantIdentification identification)
        {
            return identification._services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>();
        }

        public static IServiceCollection TenantForSourceIP(this TenantIdentification identification)
        {
            return identification._services.AddScoped<ITenantIdentificationService, SourceIPTenantIdentificationService>();
        }
    }
}
