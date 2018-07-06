using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SharedFlat
{
    public sealed class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITenantIdentificationService _service;

        public TenantMiddleware(RequestDelegate next, ITenantIdentificationService service)
        {
            this._next = next;
            this._service = service;
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.Items["Tenant"] = this._service.GetTenant(context);

            return this._next(context);
        }
    }

}
