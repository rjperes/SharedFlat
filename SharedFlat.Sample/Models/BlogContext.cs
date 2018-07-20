using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SharedFlat.Sample.Models
{
    public class BlogContext : DbContext, IDesignTimeDbContextFactory<DbContext>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var svc = this.GetService<ITenantDbContext>();
            svc?.Apply(modelBuilder, this);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseSqlServer(@"Data Source=.\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=Test;")
                .Options;

            return new BlogContext(options);
        }

        public DbSet<Blog> Blogs { get; private set; }
        public DbSet<Post> Posts { get; private set; }
    }
}
