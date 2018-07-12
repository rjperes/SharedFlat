using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace SharedFlat
{
    public sealed class TenantStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder => next(builder.UseMiddleware<TenantMiddleware>());
        }
    }
}