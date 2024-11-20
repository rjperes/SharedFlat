using Microsoft.EntityFrameworkCore;

namespace SharedFlat.EntityFrameworkCore
{
    public interface ITenantDbContext
    {
        void OnModelCreating(ModelBuilder modelBuilder, DbContext context);
        void SaveChanges(DbContext tenantDbContext);
    }
}
