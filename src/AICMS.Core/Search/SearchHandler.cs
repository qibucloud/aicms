using AICMS.Core.Models;

namespace AICMS.Core.Search;

/// <summary>
/// Search handler for filtering products
/// 搜索处理器 - 用于过滤产品
/// </summary>
public class SearchHandler
{
    /// <summary>
    /// Search products by keyword
    /// 按关键词搜索产品
    /// </summary>
    public IEnumerable<Product> SearchByKeyword(IEnumerable<Product> products, string keyword)
    {
        keyword = SearchFilters.SanitizeKeyword(keyword);
        
        if (string.IsNullOrEmpty(keyword))
            return products;

        var lowerKeyword = keyword.ToLowerInvariant();
        
        return products.Where(p =>
            p.Name.ToLowerInvariant().Contains(lowerKeyword) ||
            p.Description.ToLowerInvariant().Contains(lowerKeyword) ||
            p.Category.ToLowerInvariant().Contains(lowerKeyword) ||
            p.Sku.ToLowerInvariant().Contains(lowerKeyword)
        );
    }

    /// <summary>
    /// Filter products by category
    /// 按分类过滤产品
    /// </summary>
    public IEnumerable<Product> FilterByCategory(IEnumerable<Product> products, string category)
    {
        category = SearchFilters.SanitizeCategory(category);
        
        if (string.IsNullOrEmpty(category))
            return products;

        return products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Filter products by price range
    /// 按价格范围过滤产品
    /// </summary>
    public IEnumerable<Product> FilterByPriceRange(IEnumerable<Product> products, decimal minPrice, decimal maxPrice)
    {
        return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
    }

    /// <summary>
    /// Combined search with multiple filters
    /// 结合多个过滤条件的搜索
    /// </summary>
    public IEnumerable<Product> Search(
        IEnumerable<Product> products,
        string? keyword = null,
        string? category = null,
        decimal minPrice = 0,
        decimal maxPrice = decimal.MaxValue,
        bool onlyActive = true)
    {
        IEnumerable<Product> result = products;

        if (onlyActive)
            result = result.Where(p => p.IsActive);

        if (!string.IsNullOrEmpty(keyword))
            result = SearchByKeyword(result, keyword);

        if (!string.IsNullOrEmpty(category))
            result = FilterByCategory(result, category);

        result = FilterByPriceRange(result, minPrice, maxPrice);

        return result;
    }
}
