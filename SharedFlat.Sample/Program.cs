using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedFlat.Sample.Models;
using System.Reflection;

namespace SharedFlat.Sample
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                //.EnsureDatabaseCreated()
                .Run();

        public static IHost EnsureDatabaseCreated(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var ctx = scope.ServiceProvider.GetRequiredService<BlogContext>())
            {
                ctx.Database.EnsureCreated();
            }

            return host;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging((ctx, builder) =>
                {
                    if (ctx.HostingEnvironment.IsDevelopment())
                    {
                        builder
                            .AddConsole()
                            .AddDebug();
                    }
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "SharedFlat.Sample.Abc");
                    builder.UseStartup<Startup>();
                });
    }
}
