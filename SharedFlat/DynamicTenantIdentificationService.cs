using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SharedFlat
{
    public sealed class DynamicTenantIdentificationService : ITenantIdentificationService
    {
        private readonly Func<HttpContext, string> _currentTenant;
        private readonly Func<IEnumerable<string>> _allTenants;

        public DynamicTenantIdentificationService(Func<HttpContext, string> currentTenant, Func<IEnumerable<string>> allTenants)
        {
            if (currentTenant == null)
            {
                throw new ArgumentNullException(nameof(currentTenant));
            }

            if (allTenants == null)
            {
                throw new ArgumentNullException(nameof(allTenants));
            }

            this._currentTenant = currentTenant;
            this._allTenants = allTenants;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._allTenants();
        }

        public string GetCurrentTenant(HttpContext context)
        {
            return this._currentTenant(context);
        }
    }
}
