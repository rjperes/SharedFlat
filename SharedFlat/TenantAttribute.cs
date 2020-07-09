using System;

namespace SharedFlat
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class TenantAttribute : Attribute
    {
        public TenantAttribute(string tenant)
        {
            this.Tenant = tenant;
        }

        public string Tenant { get; }

        public override bool Match(object obj)
        {
            var other = obj as TenantAttribute;
            return other?.Tenant == this.Tenant;
        }
    }
}
