using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SharedFlat
{
    public sealed class DifferentSchemaTenantDbContext : ITenantDbContext
    {
        private readonly ITenantService _service;

        public DifferentSchemaTenantDbContext(ITenantService service)
        {
            this._service = service;
        }

        public void Apply(ModelBuilder modelBuilder, DbContext context)
        {
            var tenant = this._service.GetCurrentTenant();

            foreach (var entity in modelBuilder.Model.GetEntityTypes().Where(x => typeof(ITenantEntity).IsAssignableFrom(x.ClrType)))
            {
                entity.Relational().Schema = tenant;
            }
        }
    }

}
