using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public static class TenantIdentificationExtensions
    {
        public static IServiceCollection DynamicTenant(this ITenantIdentification identification, Func<HttpContext, string> currentTenant, IEnumerable<string> allTenants = null)
        {
            return identification.Services.AddScoped<ITenantIdentificationService>(sp => new DynamicTenantIdentificationService(currentTenant, allTenants));
        }

        public static IServiceCollection TenantForHeader(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, HeaderTenantIdentificationService>();
        }

        public static IServiceCollection TenantForHeader(this ITenantIdentification identification, Action<HeaderTenantIdentificationOption> optionsBuilder)
        {
            var options = new HeaderTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, HeaderTenantIdentificationService>(sp => new HeaderTenantIdentificationService(options));
        }

        public static IServiceCollection TenantForHost(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>();
        }

        public static IServiceCollection TenantForHost(this ITenantIdentification identification, Action<HostTenantIdentificationOption> optionsBuilder)
        {
            var options = new HostTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>(sp => new HostTenantIdentificationService(options));
        }

        public static IServiceCollection TenantForQueryString(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>();
        }

        public static IServiceCollection TenantForQueryString(this ITenantIdentification identification, Action<QueryStringTenantIdentificationOption> optionsBuilder)
        {
            var options = new QueryStringTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>(sp => new QueryStringTenantIdentificationService(options));
        }

        public static IServiceCollection TenantForSourceIP(this ITenantIdentification identification)
        {
            return identification.Services.AddScoped<ITenantIdentificationService, SourceIPTenantIdentificationService>();
        }

        public static IServiceCollection TenantForSourceIP(this ITenantIdentification identification, Action<SourceIPTenantIdentificationOption> optionsBuilder)
        {
            var options = new SourceIPTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, SourceIPTenantIdentificationService>(sp => new SourceIPTenantIdentificationService(options));
        }

        public static IServiceCollection StaticTenant(this ITenantIdentification identification, string tenant)
        {
            return identification.Services.AddScoped<ITenantIdentificationService>(sp => new StaticTenantIdentificationService(tenant));
        }
    }
}
