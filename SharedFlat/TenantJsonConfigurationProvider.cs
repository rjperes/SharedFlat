using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedFlat
{
    public sealed class TenantJsonConfigurationProvider : JsonConfigurationProvider
    {
        public TenantJsonConfigurationProvider(string path) : base(new JsonConfigurationSource { Path = path, Optional = true })
        {
        }
    }
}
