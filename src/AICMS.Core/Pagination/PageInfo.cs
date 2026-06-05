namespace AICMS.Core.Pagination;

/// <summary>
/// Page information model
/// 页面信息模型
/// </summary>
public class PageInfo
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public int PreviousPage => CurrentPage - 1;
    public int NextPage => CurrentPage + 1;
    public int StartItem => (CurrentPage - 1) * ItemsPerPage + 1;
    public int EndItem => Math.Min(CurrentPage * ItemsPerPage, TotalItems);
}
