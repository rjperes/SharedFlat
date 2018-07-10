using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SharedFlat
{
    public interface ITenantIdentificationService
    {
        string GetCurrentTenant(HttpContext context);
        IEnumerable<string> GetAllTenants();
    }

}
