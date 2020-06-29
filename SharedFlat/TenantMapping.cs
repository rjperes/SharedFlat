using System;
using System.Collections.Generic;

namespace SharedFlat
{
    public class TenantMapping
    {
        public string Default { get; set; }
        public Dictionary<string, string> Tenants { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public TenantMapping Add(string value, string tenant)
        {
            this.Tenants[value] = tenant;
            return this;
        }
    }
}
