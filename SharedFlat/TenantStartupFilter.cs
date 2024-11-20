using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace SharedFlat
{
    internal sealed class TenantStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder => next(builder.UseMiddleware<TenantMiddleware>());
        }
    }
}