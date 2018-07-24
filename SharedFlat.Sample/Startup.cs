using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedFlat.Sample.Models;
using System;

namespace SharedFlat.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTenantService()
                .AddTenantMiddleware()
                .AddTenantLocations()
                .AddTenantIdentification()
                    //.DynamicTenant(x => "abc", () => new [] { "abc", "xyz" })
                    //.TenantForQueryString()
                    //.TenantForSourceIP()
                    .TenantForHost()
                .AddTenantDbContextIdentitication()
                    //.Dummy();
                    .FilterByTenant();

            services.AddTenantConfiguration<Startup>();

            services.AddDbContext<BlogContext>(options =>
            {
                options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<PerTenantSettings>("abc", options =>
            {
                options.NumberOption = 1;
                options.StringOption = "abc";
            });

            services.Configure<PerTenantSettings>("xyz", options =>
            {
                options.NumberOption = 2;
                options.StringOption = "xyz";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            var xpto = serviceProvider.GetService<Xpto>();

            //app.UseTenants();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                loggerFactory
                    .AddConsole()
                    .AddDebug();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }  
}
