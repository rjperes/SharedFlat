using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public interface ITenantIdentification
    {
        IServiceCollection Services { get; }
    }

    internal sealed class TenantIdentification : ITenantIdentification
    {
        public IServiceCollection Services { get; }

        internal TenantIdentification(IServiceCollection services)
        {
            this.Services = services;
        }
    }
}
