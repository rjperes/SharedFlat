using Microsoft.AspNetCore.Http;

namespace SharedFlat
{
    public interface ITenantIdentificationService
    {
        string GetCurrentTenant(HttpContext context);
    }

}
