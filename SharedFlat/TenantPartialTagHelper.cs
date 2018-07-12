using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Threading.Tasks;

namespace SharedFlat
{
    [HtmlTargetElement("tenant-partial")]
    public sealed class TenantPartialTagHelper : PartialTagHelper
    {
        private readonly ITenantService _service;

        public TenantPartialTagHelper(ITenantService service, ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope) : base(viewEngine, viewBufferScope)
        {
            this._service = service;
        }

        [HtmlAttributeName("tenant")]
        public string Tenant { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tenants = this.Tenant?.Split(',') ?? Enumerable.Empty<string>();
            var tenant = this._service.GetCurrentTenant();

            if (tenants.Any(t => t == tenant))
            {
                return base.ProcessAsync(context, output);
            }

            return Task.CompletedTask;
        }
    }
}
