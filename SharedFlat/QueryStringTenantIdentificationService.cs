using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat
{
    public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly QueryStringTenantIdentificationOption _options;

        public QueryStringTenantIdentificationService(IConfiguration configuration, string tenantKey = nameof(TenantService.Tenant))
        {
            var options = new QueryStringTenantIdentificationOption();
            options.Mapping = configuration.GetTenantMapping();
            options.Parameter = tenantKey ?? options.Parameter;
            
            this._options = options;
        }

        public QueryStringTenantIdentificationService(QueryStringTenantIdentificationOption options)
        {
            this._options = options;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var tenant = context.Request.Query[this._options.Parameter].ToString();

            if (string.IsNullOrWhiteSpace(tenant) || !this._options.Mapping.Tenants.Values.Contains(tenant, StringComparer.InvariantCultureIgnoreCase))
            {
                return this._options.Mapping.Default;
            }

            if (this._options.Mapping.Tenants.TryGetValue(tenant, out var mappedTenant))
            {
                return mappedTenant;
            }

            return tenant;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._options.Mapping.Tenants.Values;
        }
    }
}
