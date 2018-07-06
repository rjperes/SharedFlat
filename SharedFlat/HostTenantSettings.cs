using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public class HostTenantSettings
    {
        public string Default { get; set; }
        public Dictionary<string, string> Tenants { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    }

}
