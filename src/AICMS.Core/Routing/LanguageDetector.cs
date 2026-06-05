namespace AICMS.Core.Routing;

/// <summary>
/// Language detector for multi-language support
/// 语言检测器 - 用于多语言支持
/// </summary>
public class LanguageDetector
{
    private readonly string[] _supportedLanguages;
    private readonly string _defaultLanguage;

    public LanguageDetector(string[] supportedLanguages = null, string defaultLanguage = "cn")
    {
        _supportedLanguages = supportedLanguages ?? new[] { "cn", "en", "jp" };
        _defaultLanguage = defaultLanguage;
    }

    /// <summary>
    /// Detect language from path
    /// 从路径检测语言
    /// </summary>
    public string DetectLanguage(string path)
    {
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        
        if (segments.Length == 0)
            return _defaultLanguage;

        var firstSegment = segments[0].ToLowerInvariant();
        
        return _supportedLanguages.Contains(firstSegment) ? firstSegment : _defaultLanguage;
    }

    /// <summary>
    /// Get localized path without language prefix
    /// 获取本地化路径（不包括语言前缀）
    /// </summary>
    public string GetLocalizedPath(string path)
    {
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        
        if (segments.Length == 0)
            return "/";

        var firstSegment = segments[0].ToLowerInvariant();
        
        if (_supportedLanguages.Contains(firstSegment))
        {
            // Remove language prefix
            var remaining = string.Join("/", segments.Skip(1));
            return "/" + remaining;
        }

        return path;
    }

    /// <summary>
    /// Build localized path
    /// 构建本地化路径
    /// </summary>
    public string BuildLocalizedPath(string language, string path)
    {
        if (language == _defaultLanguage || language == "cn")
            return path.StartsWith("/") ? path : "/" + path;

        var cleanPath = path.StartsWith("/") ? path.Substring(1) : path;
        return $"/{language}/{cleanPath}";
    }
}
