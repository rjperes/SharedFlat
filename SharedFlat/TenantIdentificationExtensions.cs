using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public static class TenantIdentificationExtensions
    {
        public static IServiceCollection DynamicTenant(this ITenantIdentification identification, Func<HttpContext, string> currentTenant, Func<IEnumerable<string>> allTenants)
        {
            return identification.Services.AddScoped<ITenantIdentificationService>(sp => new DynamicTenantIdentificationService(currentTenant, allTenants));
        }

        public static IServiceCollection TenantForHost(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>();
        }

        public static IServiceCollection TenantForQueryString(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>();
        }

        public static IServiceCollection TenantForSourceIP(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, SourceIPTenantIdentificationService>();
        }

        public static IServiceCollection StaticTenant(this ITenantIdentification identification, string tenant)
        {
            return identification.Services.AddScoped<ITenantIdentificationService>(sp => new StaticTenantIdentificationService(tenant));
        }
    }
}
