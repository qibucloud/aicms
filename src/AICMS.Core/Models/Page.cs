namespace AICMS.Core.Models;

/// <summary>
/// Page model for rendering
/// 页面模型
/// </summary>
public class Page
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Language { get; set; } = "cn";
}
