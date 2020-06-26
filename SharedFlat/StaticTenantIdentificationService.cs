using Microsoft.AspNetCore.Http;

namespace SharedFlat
{
    public sealed class StaticTenantIdentificationService : ITenantIdentificationService
    {
        public StaticTenantIdentificationService(string tenant)
        {
            this.Tenant = tenant;
        }

        public string Tenant { get; }

        public string GetCurrentTenant(HttpContext context) => Tenant;
    }
}
