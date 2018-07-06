using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SharedFlat
{
    public interface ITenantIdentificationService
    {
        string GetTenant(HttpContext context);
        IEnumerable<string> GetTenants();
    }

}
