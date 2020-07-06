using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public interface ITenantConfiguration
    {
        void ConfigureServices(IConfiguration configuration, IServiceCollection services);
        string Tenant { get; }
    }
}
