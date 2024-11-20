using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SharedFlat.Services
{
    public sealed class TenantService : ITenantService, ITenantsEnumerationService
    {
        public static readonly string Tenant = nameof(Tenant);

        private readonly HttpContext _httpContext;
        private readonly ITenantIdentificationService _service;

        public TenantService(IHttpContextAccessor accessor, ITenantIdentificationService service)
        {
            _httpContext = accessor.HttpContext;
            _service = service;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return (_service as ITenantsEnumerationService)?.GetAllTenants() ?? throw new InvalidOperationException("Current tenant service does not allow enumerating tenants.");
        }

        public string GetCurrentTenant()
        {
            return _service.GetCurrentTenant(_httpContext);
        }
    }

}
