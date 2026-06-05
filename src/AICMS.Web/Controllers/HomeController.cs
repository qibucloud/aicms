using AICMS.Core.Template;
using AICMS.Core.Routing;
using Microsoft.AspNetCore.Mvc;

namespace AICMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly LiquidTemplateEngine _engine;

        public HomeController(LiquidTemplateEngine engine)
        {
            _engine = engine;
        }

        public IActionResult Index()
        {
            var routeContext = HttpContext.Items["RouteContext"] as RouteContext ?? new RouteContext();

            var model = new
            {
                title = "Home",
                content = "欢迎来到 AICMS!",
                route = routeContext
            };

            var html = _engine.RenderTemplate("index", model);
            return Content(html, "text/html");
        }
    }
}
