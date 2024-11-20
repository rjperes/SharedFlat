using Microsoft.AspNetCore.Builder;

namespace SharedFlat.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTenants(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
