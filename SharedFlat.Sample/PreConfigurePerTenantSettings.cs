using Microsoft.Extensions.Options;
using System;

namespace SharedFlat.Sample
{

    public sealed class PreConfigurePerTenantSettings : IConfigureOptions<PerTenantSettings>, IConfigureNamedOptions<PerTenantSettings>
    {
        public PreConfigurePerTenantSettings(IServiceProvider serviceProvider)
        {

        }

        public void Configure(PerTenantSettings options)
        {
            options.ToString();
        }

        public void Configure(string name, PerTenantSettings options)
        {
            options.ToString();
        }
    }
}