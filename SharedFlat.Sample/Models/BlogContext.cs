using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharedFlat.EntityFrameworkCore;

namespace SharedFlat.Sample.Models
{
    public class BlogContext : TenantDbContext, IDesignTimeDbContextFactory<DbContext>
    {
        public BlogContext(DbContextOptions<BlogContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
        }

        public DbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("Blog")
                //.UseSqlServer(@"Data Source=.\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=Test;")
                .Options;

            return new BlogContext(options, new HttpContextAccessor { HttpContext = this.HttpContext });
        }

        public DbSet<Blog> Blogs { get; private set; }
        public DbSet<Post> Posts { get; private set; }
    }
}
