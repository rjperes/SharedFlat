using Microsoft.EntityFrameworkCore;

namespace SharedFlat
{
    internal sealed class DummyTenantDbContext : ITenantDbContext
    {
        internal static readonly ITenantDbContext Instance = new DummyTenantDbContext();

        private DummyTenantDbContext()
        {

        }

        public void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
        }

        public void SaveChanges(DbContext context)
        {
        }
    }
}