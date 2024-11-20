using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedFlat.EntityFrameworkCore.Extensions;
using SharedFlat.Extensions;
using SharedFlat.Sample.Models;

namespace SharedFlat.Sample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureWebHostDefaults(options =>
            {
                options.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "SharedFlat.Sample.Abc");
                options.Configure(app =>
                {
                    using var scope = app.ApplicationServices.CreateScope();
                    using var ctx = scope.ServiceProvider.GetRequiredService<BlogContext>();
                    ctx.Database.EnsureCreated();
                });
                options.ConfigureLogging((ctx, log) =>
                {
                    if (ctx.HostingEnvironment.IsDevelopment())
                    {
                        log.AddConsole()
                            .AddDebug();
                    }
                });
            });

            // Add services to the container.
            builder.Services
                .AddTenantService()
                .AddTenantMiddleware()  //not used, can be omitted
                .AddTenantLocations()   //for Razor views
                .AddTenantIdentification()
                //.TenantForHeader()
                //.DynamicTenant(x => "abc", new [] { "abc", "xyz" })
                //.TenantForQueryString()
                //.TenantForSourceIP()
                .TenantForHost(options =>
                {
                    builder.Configuration.BindTenantsMapping(options.Mapping);
                })
                //.StaticTenant("abc")
                .AddTenantDbContextIdentitication()
                    //.Dummy();
                    .FilterByTenant();

            builder.Services.AddTenantConfiguration();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<BlogContext>(options =>
            {
                //options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
                options.UseInMemoryDatabase("Blog");
                //.UseInternalServiceProvider(serviceProvider);
            });

            builder.Services.Configure<PerTenantSettings>("abc", options =>
            {
                builder.Configuration.BindTenantConfiguration("abc", options);
            });

            builder.Services.Configure<PerTenantSettings>("abc", options =>
            {
                options.NumberOption = 1;
                options.StringOption = "abc";
            });

            builder.Services.Configure<PerTenantSettings>("xyz", options =>
            {
                options.NumberOption = 2;
                options.StringOption = "xyz";
            });

            builder.Services.ConfigureOptions<PreConfigurePerTenantSettings>();
            builder.Services.ConfigureOptions<PostConfigurePerTenantSettings>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();

            app.Run();

        }
    }
}
