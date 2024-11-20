using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat.EntityFrameworkCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static DbContextIdentification AddTenantDbContextIdentitication(this IServiceCollection services)
        {
            return new DbContextIdentification(services);
        }
    }
}
