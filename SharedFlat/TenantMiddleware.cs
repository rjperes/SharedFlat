using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedFlat
{
    public sealed class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            this._next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Items.ContainsKey(nameof(TenantService.Tenant)) == false)
            {
                var service = context.RequestServices.GetService<ITenantIdentificationService>();
                var configuration = context.RequestServices.GetService<IConfiguration>();
                var environment = context.RequestServices.GetService<IHostingEnvironment>();
                var tenant = service.GetCurrentTenant(context);

                var providers = ((configuration as ConfigurationRoot).Providers as List<IConfigurationProvider>);
                if (providers.OfType<TenantJsonConfigurationProvider>().Any() == false)
                {
                    providers.Insert(0, new TenantJsonConfigurationProvider($"appsettings.{tenant}.{environment.EnvironmentName}"));
                }

                context.Items[nameof(TenantService.Tenant)] = tenant;
            }

            return this._next(context);
        }
    }

}
