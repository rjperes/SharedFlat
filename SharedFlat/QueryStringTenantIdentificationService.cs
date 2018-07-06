using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat
{
    public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService
    {
        private readonly HostTenantSettings _tenants;
        private readonly string _tenantKey = "Tenant";

        public QueryStringTenantIdentificationService(IConfiguration configuration, string tenantKey = "Tenant")
        {
            this._tenants = configuration.GetHostTenantSettings();
            this._tenantKey = tenantKey ?? this._tenantKey;
        }

        public string GetTenant(HttpContext context)
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

        public IEnumerable<string> GetTenants()
        {
            return this._tenants.Tenants.Values;
        }
    }

}
