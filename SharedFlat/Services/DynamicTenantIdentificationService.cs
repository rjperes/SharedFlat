using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SharedFlat.Services
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

            _currentTenant = currentTenant;
            _allTenants = allTenants ?? new List<string>();
        }

        public IEnumerable<string> GetAllTenants()
        {
            return _allTenants;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            return _currentTenant(context);
        }
    }
}
