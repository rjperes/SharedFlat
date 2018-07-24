using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SharedFlat
{
    public sealed class DifferentConnectionTenantDbContext : ITenantDbContext
    {
        private readonly ITenantService _service;
        private readonly IConfiguration _configuration;

        public DifferentConnectionTenantDbContext(ITenantService service, IConfiguration configuration)
        {
            this._service = service;
            this._configuration = configuration;
        }

        public void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            var tenant = this._service.GetCurrentTenant();
            var connectionString = this._configuration.GetConnectionString(tenant);

            context.Database.GetDbConnection().ConnectionString = connectionString;
        }

        public void SaveChanges(DbContext context)
        {
        }
    }
}
