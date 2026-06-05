using AICMS.Core.Template;
using AICMS.Core.Routing;
using Xunit;

namespace AICMS.Tests
{
    public class TemplateEngineTests
    {
        [Fact]
        public void RenderTemplate_Returns_Html_For_Simple_Template()
        {
            // Use a local temp templates path or the one configured in project
            var loader = new TemplateLoader("templates"); // ensure templates/index exists for test env
            var engine = new LiquidTemplateEngine(loader, "templates");

            var model = new { title = "Test", content = "Hello" };
            var html = engine.RenderTemplate("index", model);

            Assert.False(string.IsNullOrWhiteSpace(html));
            Assert.Contains("Hello", html);
        }
    }
}
