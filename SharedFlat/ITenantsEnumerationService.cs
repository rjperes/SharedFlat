using System.Collections.Generic;

namespace SharedFlat
{
    public interface ITenantsEnumerationService
    {
        IEnumerable<string> GetAllTenants();
    }
}
