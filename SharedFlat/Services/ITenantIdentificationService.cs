using Microsoft.AspNetCore.Http;

namespace SharedFlat.Services
{
    public interface ITenantIdentificationService
    {
        string GetCurrentTenant(HttpContext context);
    }

}
