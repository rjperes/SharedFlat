using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace SharedFlat
{
    public sealed class DifferentConnectionTenantDbContext : ITenantDbContext
    {
        private readonly ITenantService _service;

        public DifferentConnectionTenantDbContext(ITenantService service)
        {
            this._service = service;
        }

        public void Apply(ModelBuilder modelBuilder, DbContext context)
        {
            var tenant = this._service.GetCurrentTenant();
            var configuration = this._service.GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(tenant);

            context.Database.GetDbConnection().ConnectionString = connectionString;
        }
    }
}
