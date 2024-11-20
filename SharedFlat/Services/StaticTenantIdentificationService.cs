using Microsoft.AspNetCore.Http;

namespace SharedFlat.Services
{
    public sealed class StaticTenantIdentificationService : ITenantIdentificationService
    {
        public StaticTenantIdentificationService(string tenant)
        {
            Tenant = tenant ?? string.Empty;
        }

        public string Tenant { get; }

        public string GetCurrentTenant(HttpContext context) => Tenant;
    }
}
