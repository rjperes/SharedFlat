using Microsoft.Extensions.Options;
using System;

namespace SharedFlat.Sample
{
    public sealed class PostConfigurePerTenantSettings : IPostConfigureOptions<PerTenantSettings>
    {
        public PostConfigurePerTenantSettings(IServiceProvider serviceProvider)
        {

        }

        public void PostConfigure(string name, PerTenantSettings options)
        {
            options.ToString();
        }
    }
}