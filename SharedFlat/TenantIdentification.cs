using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public sealed class TenantIdentification
    {
        internal readonly IServiceCollection _services;

        internal TenantIdentification(IServiceCollection services)
        {
            this._services = services;
        }
    }
}
