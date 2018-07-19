using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedFlat.Sample.Models;
using System.Diagnostics;
using System.Linq;

namespace SharedFlat.Sample.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IOptionsSnapshot<PerTenantSettings> settings, ITenantService service)
        {
            var tenant = service.GetCurrentTenant();
            var options = settings.Get(tenant);
        }

        public IActionResult Index([FromServices] BlogContext context)
        {
            //context.Blogs.ToList();


            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
