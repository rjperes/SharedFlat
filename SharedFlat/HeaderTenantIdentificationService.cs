using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat
{
    public sealed class HeaderTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly HeaderTenantIdentificationOption _options;

        public HeaderTenantIdentificationService(IConfiguration configuration, string header = nameof(TenantService.Tenant))
        {
            this._options = new HeaderTenantIdentificationOption();
            this._options.Mapping = configuration.GetTenantMapping();
            this._options.Header = header ?? this._options.Header;
        }

        public HeaderTenantIdentificationService(HeaderTenantIdentificationOption options)
        {
            this._options = options;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var tenant = context.Request.Headers[this._options.Header].ToString();

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
