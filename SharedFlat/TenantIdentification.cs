using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    internal sealed class TenantIdentification : ITenantIdentification
    {
        public IServiceCollection Services { get; }

        internal TenantIdentification(IServiceCollection services)
        {
            this.Services = services;
        }
    }
}
