using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SharedFlat.Sample.Models
{
    public class BlogContext : DbContext, IDesignTimeDbContextFactory<DbContext>
    {
        private readonly ITenantDbContext _service;

        public BlogContext(ITenantDbContext service) : this(@"Data Source=.\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=Test;")
        {
            this._service = service;
        }

        public BlogContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        /*public BlogContext(DbContextOptions options) : base(options)
        {
        }*/

        private static DbContextOptions GetOptions(string connectionString)
        {
            var loggerFactory = new LoggerFactory()
                .AddConsole((categoryName, logLevel) =>
                    true
                    //(logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name)
                    );

            var builder = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory);

            return builder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this._service?.Apply(modelBuilder, this);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbContext CreateDbContext(string[] args)
        {
            return new BlogContext(@"Data Source=.\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=Test;");
        }

        public DbSet<Blog> Blogs { get; private set; }
        public DbSet<Post> Posts { get; private set; }
    }
}
