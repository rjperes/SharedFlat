using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace SharedFlat
{
    public sealed class HostTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly HostTenantIdentificationOption _options;

        public HostTenantIdentificationService(IConfiguration configuration)
        {
            this._options = new HostTenantIdentificationOption();
            this._options.Mapping = configuration.GetTenantMapping();
        }

        public HostTenantIdentificationService(HostTenantIdentificationOption options)
        {
            this._options = options;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._options.Mapping.Tenants.Values;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            if (!this._options.Mapping.Tenants.TryGetValue(context.Request.Host.Host, out var tenant))
            {
                tenant = this._options.Mapping.Default;
            }

            return tenant;
        }
    }
}
