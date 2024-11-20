using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedFlat.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat.Services
{
    public sealed class HeaderTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly HeaderTenantIdentificationOption _options;

        public HeaderTenantIdentificationService(IConfiguration configuration, string header = nameof(TenantService.Tenant))
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            _options = new HeaderTenantIdentificationOption();
            _options.Mapping = configuration.GetTenantMapping();
            _options.Header = header ?? _options.Header;
        }

        public HeaderTenantIdentificationService(HeaderTenantIdentificationOption options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            _options = options;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var tenant = context.Request.Headers[_options.Header].ToString();

            if (string.IsNullOrWhiteSpace(tenant) || !_options.Mapping.Tenants.Values.Contains(tenant, StringComparer.InvariantCultureIgnoreCase))
            {
                return _options.Mapping.Default;
            }

            if (_options.Mapping.Tenants.TryGetValue(tenant, out var mappedTenant))
            {
                return mappedTenant;
            }

            return tenant;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return _options.Mapping.Tenants.Values;
        }
    }
}
