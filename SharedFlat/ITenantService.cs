using System.Collections.Generic;

namespace SharedFlat
{

    public interface ITenantService
    {
        string GetCurrentTenant();
        //IEnumerable<string> GetAllTenants();
    }
}
