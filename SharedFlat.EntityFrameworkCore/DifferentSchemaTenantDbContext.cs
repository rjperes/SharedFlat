using Microsoft.EntityFrameworkCore;
using SharedFlat.Services;
using System.Linq;

namespace SharedFlat.EntityFrameworkCore
{
    public sealed class DifferentSchemaTenantDbContext : ITenantDbContext
    {
        private readonly ITenantService _service;

        public DifferentSchemaTenantDbContext(ITenantService service)
        {
            this._service = service;
        }

        public void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            var tenant = this._service.GetCurrentTenant();

            foreach (var entity in modelBuilder.Model.GetEntityTypes().Where(x => typeof(ITenantEntity).IsAssignableFrom(x.ClrType)))
            {
                entity.SetSchema(tenant);
            }
        }

        public void SaveChanges(DbContext context)
        {
        }
    }
}
