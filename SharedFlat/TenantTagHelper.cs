using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Threading.Tasks;

namespace SharedFlat
{
    [HtmlTargetElement("tenant")]
    public sealed class TenantTagHelper : TagHelper
    {
        private readonly ITenantService _service;

        public TenantTagHelper(ITenantService service)
        {
            this._service = service;
        }

        [HtmlAttributeName("name")]
        public string Name { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tenants = this.Name?.Split(',') ?? Enumerable.Empty<string>();

            if (!tenants.Any(tenant => this._service.GetCurrentTenant() == tenant))
            {
                output.SuppressOutput();
            }

            return base.ProcessAsync(context, output);
        }
    }

}
