using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat
{
    public sealed class SourceIPTenantIdentificationService : ITenantIdentificationService
    {
        private readonly TenantMapping _tenants;

        public SourceIPTenantIdentificationService(IConfiguration configuration)
        {
            this._tenants = configuration.GetTenantMapping();
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._tenants.Tenants.Values;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var tenant = this._tenants.Tenants.Keys.FirstOrDefault(x => x.StartsWith(ip));

            if (tenant != null)
            {
                return this._tenants.Tenants[tenant];
            }
            else
            {
                return this._tenants.Default;
            }
        }
    }
}
