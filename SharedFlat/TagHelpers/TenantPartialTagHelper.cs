using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SharedFlat.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SharedFlat.TagHelpers
{
    [HtmlTargetElement("tenant-partial")]
    public sealed class TenantPartialTagHelper : PartialTagHelper
    {
        private readonly ITenantService _service;

        public TenantPartialTagHelper(ITenantService service, ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope) : base(viewEngine, viewBufferScope)
        {
            _service = service;
        }

        [HtmlAttributeName("tenant")]
        public string Tenant { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tenants = Tenant?.Split(',') ?? Enumerable.Empty<string>();
            var tenant = _service.GetCurrentTenant();

            if (tenants.Any(t => t == tenant))
            {
                return base.ProcessAsync(context, output);
            }

            return Task.CompletedTask;
        }
    }
}
