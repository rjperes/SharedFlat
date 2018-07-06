using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace SharedFlat
{
    public sealed class TenantTagHelperComponent : TagHelperComponent
    {
        private readonly ITenantService _service;
        private bool _injectScript;
        private readonly bool _injectStylesheet;
        private readonly string _rootPath;

        public TenantTagHelperComponent(ITenantService service, bool injectScript, bool injectStylesheet, string rootPath = null)
        {
            this._service = service;
            this._injectScript = injectScript;
            this._injectStylesheet = injectStylesheet;
            this._rootPath = rootPath ?? "/";
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tenant = this._service.GetCurrentTenant();

            if (string.Equals(context.TagName, "head", StringComparison.Ordinal))
            {
                var root = this._rootPath;

                if (!root.EndsWith("/"))
                {
                    root = root + "/";
                }

                if (this._injectScript)
                {
                    var scriptPath = root + tenant + ".js";
                    output.PostContent.AppendHtml($"<script src=\"{scriptPath}\"></script>\r\n");
                }

                if (this._injectStylesheet)
                {
                    var scriptPath = root + tenant + ".css";
                    output.PostContent.AppendHtml($"<link rel=\"stylesheet\" href=\"{scriptPath}\" type=\"text/css\" />\r\n");
                }
            }

            return base.ProcessAsync(context, output);
        }
    }

}
