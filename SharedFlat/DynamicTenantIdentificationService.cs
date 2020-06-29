using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public sealed class DynamicTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly Func<HttpContext, string> _currentTenant;
        private readonly IEnumerable<string> _allTenants;

        public DynamicTenantIdentificationService(Func<HttpContext, string> currentTenant, IEnumerable<string> allTenants)
        {
            if (currentTenant == null)
            {
                throw new ArgumentNullException(nameof(currentTenant));
            }

            this._currentTenant = currentTenant;
            this._allTenants = allTenants ?? new List<string>();
        }

        public IEnumerable<string> GetAllTenants()
        {
            return this._allTenants;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            return this._currentTenant(context);
        }
    }
}
