using Microsoft.EntityFrameworkCore;

namespace SharedFlat
{
    public interface ITenantDbContext
    {
        void OnModelCreating(ModelBuilder modelBuilder, DbContext context);
        void SaveChanges(DbContext tenantDbContext);
    }
}
