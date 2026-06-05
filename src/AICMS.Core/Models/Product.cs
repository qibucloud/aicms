namespace AICMS.Core.Models;

/// <summary>
/// Product model
/// 产品模型
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Sku { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
