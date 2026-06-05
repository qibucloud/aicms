namespace AICMS.Core.Models;

/// <summary>
/// Navigation model for breadcrumb and menu
/// 导航模型 - 用于面包屑和菜单
/// </summary>
public class NavigationModel
{
    public string CurrentPage { get; set; } = string.Empty;
    public string Language { get; set; } = "cn";
    public List<Breadcrumb> Breadcrumbs { get; set; } = new();
    public List<MenuItem> MainMenu { get; set; } = new();
}

public class Breadcrumb
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class MenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public List<MenuItem>? SubItems { get; set; }
}
