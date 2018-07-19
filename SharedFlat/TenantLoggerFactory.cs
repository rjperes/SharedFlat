using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace SharedFlat
{
    public sealed class TenantLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }

    public sealed class TenantLoggerFactory : ILoggerFactory
    {
        private readonly ILoggerFactory _loggerFactory = new LoggerFactory();
        private readonly IServiceProvider _serviceProvider;

        public TenantLoggerFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void AddProvider(ILoggerProvider provider)
        {
            this._loggerFactory.AddProvider(provider);
        }

        public ILogger CreateLogger(string categoryName)
        {
            using (var sp = this._serviceProvider.CreateScope())
            { 
                var tenantService = sp.ServiceProvider.GetService<ITenantService>();
                var tenant = tenantService?.GetCurrentTenant();

                if (tenant != null)
                {
                    return this._loggerFactory.CreateLogger($"{tenant}:{categoryName}");
                }
                else
                {
                    return this._loggerFactory.CreateLogger(categoryName);
                }
            }
        }

        public void Dispose()
        {
            this._loggerFactory.Dispose();
        }
    }
}