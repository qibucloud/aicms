namespace AICMS.Core.Routing;

/// <summary>
/// Language router for handling multi-language routes
/// 语言路由器 - 处理多语言路由
/// </summary>
public class LanguageRouter
{
    private readonly LanguageDetector _languageDetector;
    private readonly Dictionary<string, Dictionary<string, string>> _routeMap;

    public LanguageRouter(LanguageDetector languageDetector)
    {
        _languageDetector = languageDetector;
        _routeMap = new Dictionary<string, Dictionary<string, string>>();
        InitializeRoutes();
    }

    /// <summary>
    /// Initialize language-specific routes
    /// 初始化语言特定的路由
    /// </summary>
    private void InitializeRoutes()
    {
        // Chinese routes
        _routeMap["cn"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "/", "index" },
            { "/关于", "about" },
            { "/产品", "products" },
        };

        // English routes
        _routeMap["en"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "/", "index" },
            { "/about", "about" },
            { "/products", "products" },
        };

        // Japanese routes
        _routeMap["jp"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "/", "index" },
            { "/概要", "about" },
            { "/商品", "products" },
        };
    }

    /// <summary>
    /// Parse route and return RouteContext
    /// 解析路由并返回 RouteContext
    /// </summary>
    public RouteContext ParseRoute(string path, Dictionary<string, string> queryParameters)
    {
        var language = _languageDetector.DetectLanguage(path);
        var localizedPath = _languageDetector.GetLocalizedPath(path);
        
        var context = new RouteContext
        {
            Language = language,
            OriginalPath = path,
            LocalizedPath = localizedPath,
            QueryParameters = queryParameters
        };

        // Extract page name
        var page = ResolvePageName(localizedPath, language);
        context.Page = page;

        // Extract pagination
        if (localizedPath.Contains("-page-") || localizedPath.Contains("-ページ"))
        {
            var pageMatch = ExtractPageNumber(localizedPath);
            if (pageMatch > 0)
                context.CurrentPage = pageMatch;
        }

        return context;
    }

    /// <summary>
    /// Resolve page name from path
    /// 从路径解析页面名称
    /// </summary>
    private string ResolvePageName(string path, string language)
    {
        var cleanPath = path.ToLowerInvariant().TrimEnd('/');
        if (string.IsNullOrEmpty(cleanPath) || cleanPath == "/")
            return "index";

        if (cleanPath.Contains("-page-") || cleanPath.Contains("-ページ"))
        {
            cleanPath = cleanPath.Split("-page-", StringSplitOptions.None)[0]
                                 .Split("-ページ", StringSplitOptions.None)[0]
                                 .Trim('/');
        }

        var routes = _routeMap[language];
        
        if (routes.TryGetValue(cleanPath, out var pageName))
            return pageName;

        // Remove leading slash for comparison
        cleanPath = cleanPath.StartsWith("/") ? cleanPath.Substring(1) : cleanPath;
        
        foreach (var route in routes)
        {
            if (route.Key.TrimStart('/').Equals(cleanPath, StringComparison.OrdinalIgnoreCase))
                return route.Value;
        }

        return cleanPath.Split('/')[0];
    }

    /// <summary>
    /// Extract page number from path
    /// 从路径提取页码
    /// </summary>
    private int ExtractPageNumber(string path)
    {
        var patterns = new[] { "-page-", "-ページ" };
        
        foreach (var pattern in patterns)
        {
            if (path.Contains(pattern))
            {
                var parts = path.Split(pattern, StringSplitOptions.None);
                if (parts.Length > 1 && int.TryParse(parts[1].Split('/')[0], out var pageNum))
                    return pageNum;
            }
        }

        return 1;
    }

    /// <summary>
    /// Build URL for a page with language and pagination
    /// 为页面构建带有语言和分页的URL
    /// </summary>
    public string BuildUrl(string language, string page, int pageNum = 1, Dictionary<string, string>? queryParams = null)
    {
        var baseUrl = _languageDetector.BuildLocalizedPath(language, $"/{page}");
        
        if (pageNum > 1)
        {
            var pagePattern = language == "jp" ? $"-ページ{pageNum}" : $"-page-{pageNum}";
            baseUrl = baseUrl.TrimEnd('/') + pagePattern;
        }

        if (queryParams?.Count > 0)
        {
            var queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            baseUrl = $"{baseUrl}?{queryString}";
        }

        return baseUrl;
    }
}
