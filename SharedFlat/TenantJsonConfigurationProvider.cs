using Microsoft.Extensions.Configuration.Json;

namespace SharedFlat
{
    public sealed class TenantJsonConfigurationProvider : JsonConfigurationProvider
    {
        public TenantJsonConfigurationProvider(string path) : base(new JsonConfigurationSource { Path = path, Optional = true })
        {
        }

        public override void Load()
        {
            base.Load();
        }
    }
}
