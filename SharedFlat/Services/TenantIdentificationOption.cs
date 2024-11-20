namespace SharedFlat.Services
{
    public abstract class TenantIdentificationOption
    {
        public TenantMapping Mapping { get; set; } = new TenantMapping();
    }
}
