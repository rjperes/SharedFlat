﻿namespace SharedFlat
{
    public class QueryStringTenantIdentificationOption : TenantIdentificationOption
    {
        public string Parameter { get; set; } = nameof(TenantService.Tenant);        
    }
}
