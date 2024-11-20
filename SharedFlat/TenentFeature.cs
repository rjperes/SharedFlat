using System;

namespace SharedFlat
{
    public interface ITenantFeature
    {
        string Tenant { get; }
    }

    internal class TenantFeature : ITenantFeature
    {
        public TenantFeature(string tenant)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(tenant, nameof(tenant));
            Tenant = tenant;
        }

        public string Tenant { get; }
    }
}
