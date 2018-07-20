using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SharedFlat
{
    public interface ITenantConfiguration
    {
        void Configure(IConfiguration configuration);
        void ConfigureServices(IServiceCollection services);
        string Tenant { get; }
    }
}
