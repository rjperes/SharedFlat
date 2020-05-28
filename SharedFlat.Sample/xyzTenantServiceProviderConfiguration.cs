using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat.Sample
{
    public sealed class xyzTenantServiceProviderConfiguration : ITenantConfiguration
    {
        public void Configure(IConfiguration configuration)
        {
            configuration["Tenant"] = this.Tenant;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Xpto>(sp => new Xpto { Name = this.Tenant });
        }

        public string Tenant { get; } = "xyz";
    }
}
