using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat
{
    public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService
    {
        private readonly TenantMapping _tenants;
        private readonly string _tenantKey = nameof(TenantService.Tenant);

        public QueryStringTenantIdentificationService(IConfiguration configuration, string tenantKey = nameof(TenantService.Tenant))
        {
            this._tenants = configuration.GetTenantMapping();
            this._tenantKey = tenantKey ?? this._tenantKey;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var tenant = context.Request.Query[this._tenantKey].ToString();

            if (string.IsNullOrWhiteSpace(tenant) || !this._tenants.Tenants.Values.Contains(tenant, StringComparer.InvariantCultureIgnoreCase))
            {
                return this._tenants.Default;
            }

            if (this._tenants.Tenants.TryGetValue(tenant, out var mappedTenant))
            {
                return mappedTenant;
            }

            return tenant;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._tenants.Tenants.Values;
        }
    }
}
