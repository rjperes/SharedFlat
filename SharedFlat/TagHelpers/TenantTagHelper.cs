using Microsoft.AspNetCore.Razor.TagHelpers;
using SharedFlat.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SharedFlat.TagHelpers
{
    [HtmlTargetElement("tenant")]
    public sealed class TenantTagHelper : TagHelper
    {
        private readonly ITenantService _service;

        public TenantTagHelper(ITenantService service)
        {
            _service = service;
        }

        [HtmlAttributeName("name")]
        public string Name { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tenants = Name?.Split(',') ?? Enumerable.Empty<string>();
            var tenant = _service.GetCurrentTenant();

            if (!tenants.Any(t => t == tenant))
            {
                output.SuppressOutput();
            }

            return base.ProcessAsync(context, output);
        }
    }

}
