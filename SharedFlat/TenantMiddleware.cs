using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace SharedFlat
{
    public sealed class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Items.ContainsKey(nameof(TenantService.Tenant)) == false)
            {
                var service = context.RequestServices.GetService<ITenantIdentificationService>();
                var tenant = service.GetCurrentTenant(context);
                /*var configuration = context.RequestServices.GetService<IConfiguration>();
                var environment = context.RequestServices.GetService<IHostingEnvironment>();

                var providers = ((configuration as ConfigurationRoot).Providers as List<IConfigurationProvider>);
                var provider = providers.OfType<TenantJsonConfigurationProvider>().SingleOrDefault();

                if (provider == null)
                {
                    providers.Insert(0, new TenantJsonConfigurationProvider($"appsettings.{tenant}.{environment.EnvironmentName}.json"));
                }
                else
                {
                    provider.Source.Path = $"appsettings.{tenant}.{environment.EnvironmentName}.json";
                }*/

                //context.Items[nameof(TenantService.Tenant)] = tenant;
            }

            return this._next(context);
        }
    }

}
