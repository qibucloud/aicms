namespace AICMS.Core.Search;

/// <summary>
/// Search filters for query sanitization
/// 搜索过滤器 - 用于查询消毒
/// </summary>
public class SearchFilters
{
    /// <summary>
    /// Sanitize search keyword
    /// 消毒搜索关键词
    /// </summary>
    public static string SanitizeKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return string.Empty;

        // Remove leading/trailing whitespace
        keyword = keyword.Trim();

        // Limit length
        if (keyword.Length > 100)
            keyword = keyword.Substring(0, 100);

        // Remove potentially dangerous characters
        var forbiddenChars = new[] { '<', '>', '"', '\'', '%', ';', '&', '|', '\\', '$', '`' };
        foreach (var c in forbiddenChars)
            keyword = keyword.Replace(c.ToString(), string.Empty);

        return keyword;
    }

    /// <summary>
    /// Extract and sanitize category filter
    /// 提取并消毒分类过滤器
    /// </summary>
    public static string SanitizeCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return string.Empty;

        category = category.Trim();
        
        if (category.Length > 50)
            category = category.Substring(0, 50);

        var forbiddenChars = new[] { '<', '>', '"', '\'', '%', ';', '&', '|', '\\', '$', '`' };
        foreach (var c in forbiddenChars)
            category = category.Replace(c.ToString(), string.Empty);

        return category;
    }

    /// <summary>
    /// Parse price range filter
    /// 解析价格范围过滤器
    /// </summary>
    public static (decimal min, decimal max) ParsePriceRange(string? priceRange)
    {
        if (string.IsNullOrWhiteSpace(priceRange))
            return (0, decimal.MaxValue);

        var parts = priceRange.Split('-');
        
        if (parts.Length != 2)
            return (0, decimal.MaxValue);

        var minParsed = decimal.TryParse(parts[0], out var min);
        var maxParsed = decimal.TryParse(parts[1], out var max);

        if (!minParsed || !maxParsed || min < 0 || max < 0 || min > max)
            return (0, decimal.MaxValue);

        return (min, max);
    }
}
