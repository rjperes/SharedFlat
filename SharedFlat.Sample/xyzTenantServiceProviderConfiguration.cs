using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat.Sample
{
    public sealed class xyzTenantServiceProviderConfiguration : ITenantConfiguration
    {
        public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            
        }

        public string Tenant { get; } = "xyz";
    }
}
