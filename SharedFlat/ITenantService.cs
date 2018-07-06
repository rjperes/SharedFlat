using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace SharedFlat
{

    public interface ITenantService : IInfrastructure<IServiceProvider>
    {
        string GetCurrentTenant();
        IEnumerable<string> GetTenants();
    }
}
