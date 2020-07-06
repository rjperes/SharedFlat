using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SharedFlat
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTenantService<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime, string tenant)
        {
            services.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
            return services;
        }

        public static IServiceCollection AddTenantService(this IServiceCollection services, ServiceLifetime lifetime, Type serviceType, Type implementationType, string tenant)
        {
            services.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
            return services;
        }

        public static IServiceCollection AddTenantService<TService>(this IServiceCollection services, ServiceLifetime lifetime, Func<IServiceProvider, TService> factory, string tenant)
        {
            services.Add(new ServiceDescriptor(typeof(TService), (sp) => factory(sp), lifetime));
            return services;
        }



        private static ContainerConfiguration AddFromPath(string path, AttributedModelProvider conventions, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var assemblyFiles = Directory
                .GetFiles(path, "*.dll", searchOption);

            var assemblies = assemblyFiles
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies, conventions);

            return configuration;
        }

        public static IServiceCollection AddDynamicTenants(this IServiceCollection services, string path = null)
        {
            var conventions = new ConventionBuilder();
            var builder = conventions
                .ForTypesDerivedFrom<ITenantLoader>()
                .Export<ITenantLoader>();

            path = path ?? AppContext.BaseDirectory;

            var configuration = AddFromPath(path, conventions);

            using (var container = configuration.CreateContainer())
            {
                var svcs = container.GetExports<ITenantLoader>();

                foreach (var svc in svcs)
                {
                    svc.Register(services);

                    services.AddScoped<ITenantLoader>(sp => svc);

                    services.AddScoped<ITenantConfiguration>(sp => svc.GetTenantConfiguration());
                }
            }

            return services;
        }

        public static IServiceCollection AddTenantConfiguration(this IServiceCollection services)
        {
            return services.AddTenantConfiguration(Assembly.GetEntryAssembly());
        }

        public static IServiceCollection AddTenantConfiguration(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(type => typeof(ITenantConfiguration).IsAssignableFrom(type))
                .Where(type => (type.IsAbstract == false) && (type.IsInterface == false));

            if (types.Any())
            {
                services.AddScoped(typeof(ITenantConfiguration), sp =>
                {
                    var svc = sp.GetRequiredService<ITenantService>();
                    var configuration = sp.GetRequiredService<IConfiguration>();
                    var tenant = svc.GetCurrentTenant();
                    var instance = types
                        .Select(type => ActivatorUtilities.CreateInstance(sp, type))
                        .OfType<ITenantConfiguration>()
                        .SingleOrDefault(x => x.Tenant == tenant);

                    if (instance != null)
                    {
                        configuration[nameof(TenantService.Tenant)] = tenant;

                        instance.ConfigureServices(configuration, services);

                        sp.GetRequiredService<IHttpContextAccessor>().HttpContext.RequestServices = services.BuildServiceProvider();
                        
                        return instance;
                    }
                    else
                    {
                        return DummyTenantServiceProviderConfiguration.Instance;
                    }

                });
            }

            return services;
        }

        public static IServiceCollection AddTenantConfiguration<T>(this IServiceCollection services)
        {
            var assembly = typeof(T).Assembly;
            return services.AddTenantConfiguration(assembly);
        }

        public static ITenantIdentification AddTenantIdentification(this IServiceCollection services)
        {
            return new TenantIdentification(services);
            ;
        }

        public static IServiceCollection AddTenantLocations(this IServiceCollection services)
        {
            return services.Configure<RazorViewEngineOptions>(options =>
            {
                if (!(options.ViewLocationExpanders.FirstOrDefault() is TenantViewLocationExpander))
                {
                    options.ViewLocationExpanders.Insert(0, TenantViewLocationExpander.Instance);
                }
            });
        }

        public static IServiceCollection AddTenantService(this IServiceCollection services)
        {
            return services
                .AddHttpContextAccessor()
                .AddScoped<ITenantsEnumerationService>(sp => sp.GetRequiredService<ITenantService>() as ITenantsEnumerationService ?? throw new InvalidOperationException("Current tenant service does not allow enumerating tenants."))
                .AddScoped<ITenantService, TenantService>();
        }

        public static IServiceCollection AddQueryStringIdentificationService(this IServiceCollection services, string tenantKey = nameof(TenantService.Tenant))
        {
            return services.AddSingleton<ITenantIdentificationService, QueryStringTenantIdentificationService>(sp => new QueryStringTenantIdentificationService(sp.GetRequiredService<IConfiguration>(), tenantKey));
        }

        public static IServiceCollection AddHostIdentificationService(this IServiceCollection services)
        {
            return services.AddSingleton<ITenantIdentificationService, HostTenantIdentificationService>();
        }

        public static DbContextIdentification AddTenantDbContextIdentitication(this IServiceCollection services)
        {
            return new DbContextIdentification(services);
        }

        public static IServiceCollection AddTenantMiddleware(this IServiceCollection services)
        {
            return services.AddSingleton<IStartupFilter, TenantStartupFilter>();
        }

        public static IServiceCollection ConfigureTenant<T>(this IServiceCollection services, string tenant, Action<T> configureOptions) where T : class, ITenantOptions
        {
            return services.Configure(tenant, configureOptions);
        }
    }
}
