using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public interface ITenantIdentification
    {
        IServiceCollection Services { get; }
    }
}