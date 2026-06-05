using System.Linq;
using AICMS.Core.Models;
using AICMS.Core.Pagination;
using AICMS.Core.Routing;
using AICMS.Core.Search;
using AICMS.Core.Template;
using Microsoft.AspNetCore.Mvc;

namespace AICMS.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly LiquidTemplateEngine _engine;
        private readonly PaginationHandler _pagination;
        private readonly SearchHandler _search;

        // In-memory sample product store for demo purposes
        private static readonly List<Product> _sampleProducts = Enumerable.Range(1, 50).Select(i => new Product
        {
            Id = i,
            Name = $"Product {i}",
            Description = $"Description for product {i}",
            Category = i % 2 == 0 ? "Electronics" : "General",
            Price = 9.99m + i,
            CreatedAt = DateTime.UtcNow.AddDays(-i),
            Sku = $"SKU{i:000}"
        }).ToList();

        public ProductsController(LiquidTemplateEngine engine, PaginationHandler pagination, SearchHandler search)
        {
            _engine = engine;
            _pagination = pagination;
            _search = search;
        }

        public IActionResult Index()
        {
            var routeContext = HttpContext.Items["RouteContext"] as RouteContext ?? new RouteContext();
            var keyword = routeContext.GetQueryParameter("keyword") ?? string.Empty;
            var page = routeContext.CurrentPage > 0 ? routeContext.CurrentPage : 1;

            // Apply search/filtering (simple sanitized filter via SearchFilters inside SearchHandler)
            var filtered = _search == null || string.IsNullOrEmpty(keyword)
                ? _sampleProducts
                : _sampleProducts.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                          || p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            var pageInfo = _pagination.GetPageInfo(page, filtered.Count);
            var items = _pagination.GetPagedItems(filtered, page).ToList();
            var pageRange = _pagination.GetPageRange(pageInfo.CurrentPage, pageInfo.TotalPages);

            var model = new
            {
                title = "Products",
                products = items,
                pageInfo,
                pageRange,
                route = routeContext
            };

            var html = _engine.RenderTemplate("products", model);
            return Content(html, "text/html");
        }
    }
}
