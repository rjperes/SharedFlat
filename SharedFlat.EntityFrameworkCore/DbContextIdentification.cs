using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat.EntityFrameworkCore
{
    public sealed class DbContextIdentification
    {
        internal readonly IServiceCollection _services;

        internal DbContextIdentification(IServiceCollection services)
        {
            this._services = services;
        }
    }
}