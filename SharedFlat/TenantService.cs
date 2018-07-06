using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public sealed class TenantService : ITenantService
    {
        private readonly HttpContext _httpContext;
        private readonly ITenantIdentificationService _service;

        public IServiceProvider Instance { get; }

        public TenantService(IHttpContextAccessor accessor, ITenantIdentificationService service)
        {
            this._httpContext = accessor.HttpContext;
            this._service = service;
            this.Instance = this._httpContext.RequestServices;
        }

        public IEnumerable<string> GetTenants()
        {
            return this._service.GetTenants();
        }

        public string GetCurrentTenant()
        {
            return this._httpContext.Items["Tenant"] as string;
        }
    }

}
