using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public static class DbContextIdentificationExtensions
    {
        public static IServiceCollection DifferentConnectionForTenant(this DbContextIdentification identification)
        {
            return identification._services.AddScoped<ITenantDbContext, DifferentConnectionTenantDbContext>();
        }

        public static IServiceCollection DifferentSchemaForTenant(this DbContextIdentification identification)
        {
            return identification._services.AddScoped<ITenantDbContext, DifferentSchemaTenantDbContext>();
        }

        public static IServiceCollection FilterByTenant(this DbContextIdentification identification, string tenantColumn = nameof(TenantService.Tenant))
        {
            return identification._services.AddScoped<ITenantDbContext, FilterTenantDbContext>(sp => new FilterTenantDbContext(sp.GetRequiredService<ITenantService>(), tenantColumn));
        }
    }
}
