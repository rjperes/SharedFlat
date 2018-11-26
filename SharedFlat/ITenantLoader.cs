using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public interface ITenantLoader
    {
        void Register(IServiceCollection services);
        ITenantConfiguration GetTenantConfiguration();
    }
}