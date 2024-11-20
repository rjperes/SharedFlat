using System.Collections.Generic;

namespace SharedFlat.Services
{
    public interface ITenantsEnumerationService
    {
        IEnumerable<string> GetAllTenants();
    }
}
