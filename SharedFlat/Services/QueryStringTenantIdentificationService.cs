using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedFlat.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat.Services
{
    public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly QueryStringTenantIdentificationOption _options;

        public QueryStringTenantIdentificationService(IConfiguration configuration, string tenantKey = nameof(TenantService.Tenant))
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            var options = new QueryStringTenantIdentificationOption();
            options.Mapping = configuration.GetTenantMapping();
            options.Parameter = tenantKey ?? options.Parameter;

            _options = options;
        }

        public QueryStringTenantIdentificationService(QueryStringTenantIdentificationOption options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            _options = options;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var tenant = context.Request.Query[_options.Parameter].ToString();

            if (string.IsNullOrWhiteSpace(tenant) || !_options.Mapping.Tenants.Values.Contains(tenant, StringComparer.InvariantCultureIgnoreCase))
            {
                return _options.Mapping.Default;
            }

            if (_options.Mapping.Tenants.TryGetValue(tenant, out var mappedTenant))
            {
                return mappedTenant;
            }

            return tenant;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return _options.Mapping.Tenants.Values;
        }
    }
}
