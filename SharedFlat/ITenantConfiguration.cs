using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public interface ITenantConfiguration
    {
        void Configure(IConfiguration configuration);
        void ConfigureServices(IServiceCollection services);
        string Tenant { get; }
    }
}
