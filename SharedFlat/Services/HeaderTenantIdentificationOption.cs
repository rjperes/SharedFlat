namespace SharedFlat.Services
{
    public class HeaderTenantIdentificationOption : TenantIdentificationOption
    {
        public string Header { get; set; } = nameof(TenantService.Tenant);
    }
}
