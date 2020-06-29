using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat
{
    public sealed class SourceIPTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly SourceIPTenantIdentificationOption _options;

        public SourceIPTenantIdentificationService(IConfiguration configuration)
        {
            this._options = new SourceIPTenantIdentificationOption();
            this._options.Mapping = configuration.GetTenantMapping();
        }

        public SourceIPTenantIdentificationService(SourceIPTenantIdentificationOption options)
        {
            this._options = options;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._options.Mapping.Tenants.Values;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var tenant = this._options.Mapping.Tenants.Keys.FirstOrDefault(x => x.StartsWith(ip));

            if (tenant != null)
            {
                return this._options.Mapping.Tenants[tenant];
            }
            else
            {
                return this._options.Mapping.Default;
            }
        }
    }
}
