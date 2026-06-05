namespace AICMS.Core.Routing;

/// <summary>
/// Route context for handling requests
/// 路由上下文 - 用于处理请求
/// </summary>
public class RouteContext
{
    public string Language { get; set; } = "cn";
    public string Page { get; set; } = string.Empty;
    public int CurrentPage { get; set; } = 1;
    public Dictionary<string, string> QueryParameters { get; set; } = new();
    public string OriginalPath { get; set; } = string.Empty;
    public string LocalizedPath { get; set; } = string.Empty;

    /// <summary>
    /// Get query parameter value
    /// 获取查询参数值
    /// </summary>
    public string? GetQueryParameter(string key)
    {
        return QueryParameters.TryGetValue(key, out var value) ? value : null;
    }
}
