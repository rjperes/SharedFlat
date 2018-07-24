using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SharedFlat.Sample.Models
{
    public class BlogContext : TenantDbContext, IDesignTimeDbContextFactory<DbContext>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
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
