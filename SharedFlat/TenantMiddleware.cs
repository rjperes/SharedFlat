using Microsoft.AspNetCore.Http;
using SharedFlat.Services;
using System.Threading.Tasks;

namespace SharedFlat
{
    internal sealed class TenantMiddleware(RequestDelegate Next, ITenantIdentificationService TenantIdentificationService)
    {
        public Task InvokeAsync(HttpContext context)
        {
            if (context.Items.ContainsKey(nameof(TenantService.Tenant)) == false)
            {
                var tenant = TenantIdentificationService.GetCurrentTenant(context);
                context.Features.Set<ITenantFeature>(new TenantFeature(tenant));
                context.Items[nameof(TenantService.Tenant)] = tenant;
            }

            return Next(context);
        }
    }
}
