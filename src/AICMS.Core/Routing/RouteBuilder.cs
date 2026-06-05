namespace AICMS.Core.Routing;

/// <summary>
/// Route builder for fluent route configuration
/// 路由构建器 - 用于流畅的路由配置
/// </summary>
public class RouteBuilder
{
    private readonly LanguageRouter _router;

    public RouteBuilder(LanguageRouter router)
    {
        _router = router;
    }

    /// <summary>
    /// Build URL
    /// 构建URL
    /// </summary>
    public string Url(string page, string language = "cn", int pageNum = 1)
    {
        return _router.BuildUrl(language, page, pageNum);
    }

    /// <summary>
    /// Build URL with query parameters
    /// 构建带查询参数的URL
    /// </summary>
    public string UrlWithQuery(string page, Dictionary<string, string> queryParams, string language = "cn", int pageNum = 1)
    {
        return _router.BuildUrl(language, page, pageNum, queryParams);
    }
}
