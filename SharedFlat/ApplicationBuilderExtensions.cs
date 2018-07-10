using Microsoft.AspNetCore.Builder;

namespace SharedFlat
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTenants(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
