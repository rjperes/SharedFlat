using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SharedFlat.EntityFrameworkCore
{
    public abstract class TenantDbContext : DbContext
    {
        protected HttpContext HttpContext { get; }

        protected TenantDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor = default) : base(options)
        {
            this.HttpContext = httpContextAccessor?.HttpContext;
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
