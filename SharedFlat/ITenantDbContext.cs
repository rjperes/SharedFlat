using Microsoft.EntityFrameworkCore;

namespace SharedFlat
{
    public interface ITenantDbContext
    {
        void Apply(ModelBuilder modelBuilder, DbContext context);
    }

}
