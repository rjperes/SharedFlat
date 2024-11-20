using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SharedFlat.Services;
using System;
using System.Collections.Generic;

namespace SharedFlat.Extensions
{
    public static class TenantIdentificationExtensions
    {
        public static IServiceCollection DynamicTenant(this ITenantIdentification identification, Func<HttpContext, string> currentTenant, IEnumerable<string> allTenants = null)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            return identification.Services.AddScoped<ITenantIdentificationService>(sp => new DynamicTenantIdentificationService(currentTenant, allTenants));
        }

        public static IServiceCollection TenantForHeader(this ITenantIdentification identification)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            return identification.Services.AddScoped<ITenantIdentificationService, HeaderTenantIdentificationService>();
        }

        public static IServiceCollection TenantForHeader(this ITenantIdentification identification, Action<HeaderTenantIdentificationOption> optionsBuilder)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            var options = new HeaderTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, HeaderTenantIdentificationService>(sp => new HeaderTenantIdentificationService(options));
        }

        public static IServiceCollection TenantForHost(this ITenantIdentification identification)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            return identification.Services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>();
        }

        public static IServiceCollection TenantForHost(this ITenantIdentification identification, Action<HostTenantIdentificationOption> optionsBuilder)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            var options = new HostTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>(sp => new HostTenantIdentificationService(options));
        }

        public static IServiceCollection TenantForQueryString(this ITenantIdentification identification)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            return identification.Services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>();
        }

        public static IServiceCollection TenantForQueryString(this ITenantIdentification identification, Action<QueryStringTenantIdentificationOption> optionsBuilder)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            var options = new QueryStringTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, QueryStringTenantIdentificationService>(sp => new QueryStringTenantIdentificationService(options));
        }

        public static IServiceCollection TenantForSourceIP(this ITenantIdentification identification)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            return identification.Services.AddScoped<ITenantIdentificationService, SourceIPTenantIdentificationService>();
        }

        public static IServiceCollection TenantForSourceIP(this ITenantIdentification identification, Action<SourceIPTenantIdentificationOption> optionsBuilder)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            var options = new SourceIPTenantIdentificationOption();
            optionsBuilder?.Invoke(options);
            return identification.Services.AddScoped<ITenantIdentificationService, SourceIPTenantIdentificationService>(sp => new SourceIPTenantIdentificationService(options));
        }

        public static IServiceCollection StaticTenant(this ITenantIdentification identification, string tenant)
        {
            ArgumentNullException.ThrowIfNull(identification, nameof(identification));
            return identification.Services.AddScoped<ITenantIdentificationService>(sp => new StaticTenantIdentificationService(tenant));
        }
    }
}
