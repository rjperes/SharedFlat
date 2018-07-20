using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    internal sealed class DummyTenantServiceProviderConfiguration : ITenantConfiguration
    {
        internal static readonly ITenantConfiguration Instance = new DummyTenantServiceProviderConfiguration();

        private DummyTenantServiceProviderConfiguration()
        {
        }

        public string Tenant => string.Empty;

        public void Configure(IConfiguration configuration)
        {        
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
