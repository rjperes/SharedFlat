using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using SharedFlat.Extensions;
using System;
using System.Collections.Generic;

namespace SharedFlat.Services
{
    public sealed class HostTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly HostTenantIdentificationOption _options;

        public HostTenantIdentificationService(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            _options = new HostTenantIdentificationOption();
            _options.Mapping = configuration.GetTenantMapping();
        }

        public HostTenantIdentificationService(HostTenantIdentificationOption options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            _options = options;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return _options.Mapping.Tenants.Values;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            
            if (!_options.Mapping.Tenants.TryGetValue(context.Request.Host.Host, out var tenant))
            {
                tenant = _options.Mapping.Default;
            }

            return tenant;
        }
    }
}
