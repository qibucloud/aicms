using AICMS.Core.Routing;
using Xunit;

namespace AICMS.Tests
{
    public class RoutingTests
    {
        [Fact]
        public void LanguageDetector_Detects_Language_Prefix()
        {
            var detector = new LanguageDetector(new[] { "cn", "en", "jp" }, "cn");
            Assert.Equal("en", detector.DetectLanguage("/en/about"));
            Assert.Equal("jp", detector.DetectLanguage("/jp/商品"));
            Assert.Equal("cn", detector.DetectLanguage("/关于"));
        }

        [Fact]
        public void LanguageRouter_Parses_Page_And_Pagination()
        {
            var detector = new LanguageDetector(new[] { "cn", "en", "jp" }, "cn");
            var router = new LanguageRouter(detector);

            var ctx = router.ParseRoute("/en/products-page-2", new Dictionary<string, string>());
            Assert.Equal("en", ctx.Language);
            Assert.Equal("products", ctx.Page);
            Assert.Equal(2, ctx.CurrentPage);

            ctx = router.ParseRoute("/产品-page-3", new Dictionary<string, string>());
            Assert.Equal("cn", ctx.Language);
            Assert.Equal("products", ctx.Page);
            Assert.Equal(3, ctx.CurrentPage);
        }
    }
}
