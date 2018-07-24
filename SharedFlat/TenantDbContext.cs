using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace SharedFlat
{
    public abstract class TenantDbContext : DbContext
    {
        protected TenantDbContext(DbContextOptions options) : base(options)
        {
        }

        private TenantDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var svc = this.GetService<ITenantDbContext>();
            svc?.OnModelCreating(modelBuilder, this);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var svc = this.GetService<ITenantDbContext>();
            svc.SaveChanges(this);

            return base.SaveChanges();
        }
    }
}
