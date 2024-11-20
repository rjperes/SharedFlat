using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedFlat.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedFlat.Services
{
    public sealed class SourceIPTenantIdentificationService : ITenantIdentificationService, ITenantsEnumerationService
    {
        private readonly SourceIPTenantIdentificationOption _options;

        public SourceIPTenantIdentificationService(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            _options = new SourceIPTenantIdentificationOption();
            _options.Mapping = configuration.GetTenantMapping();
        }

        public SourceIPTenantIdentificationService(SourceIPTenantIdentificationOption options)
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
            var ip = context.Connection.RemoteIpAddress.ToString();
            var tenant = _options.Mapping.Tenants.Keys.FirstOrDefault(x => x.StartsWith(ip));

            if (tenant != null)
            {
                return _options.Mapping.Tenants[tenant];
            }
            else
            {
                return _options.Mapping.Default;
            }
        }
    }
}
