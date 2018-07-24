using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFlat
{
    public sealed class TenantViewLocationExpander : IViewLocationExpander
    {
        internal static readonly IViewLocationExpander Instance = new TenantViewLocationExpander();

        private ITenantService _service;
        private string _tenant;

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Replace("{0}", this._tenant + "/{0}");
                yield return location;
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            this._service = context.ActionContext.HttpContext.RequestServices.GetService<ITenantService>();
            this._tenant = this._service.GetCurrentTenant();
        }
    }
}
