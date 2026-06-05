using AICMS.Core.Routing;
using AICMS.Core.Template;
using Microsoft.AspNetCore.Mvc;

namespace AICMS.Web.Controllers
{
    public class AboutController : Controller
    {
        private readonly LiquidTemplateEngine _engine;

        public AboutController(LiquidTemplateEngine engine)
        {
            _engine = engine;
        }

        public IActionResult Index()
        {
            var routeContext = HttpContext.Items["RouteContext"] as RouteContext ?? new RouteContext();

            var model = new
            {
                title = "About",
                content = "关于我们 — AICMS 是一个高性能的内容管理系统。",
                route = routeContext
            };

            var html = _engine.RenderTemplate("about", model);
            return Content(html, "text/html");
        }
    }
}
