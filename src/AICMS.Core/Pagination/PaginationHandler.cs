namespace AICMS.Core.Pagination;

/// <summary>
/// Pagination handler for managing page data
/// 分页处理器 - 用于管理页面数据
/// </summary>
public class PaginationHandler
{
    private readonly int _itemsPerPage;

    public PaginationHandler(int itemsPerPage = 10)
    {
        _itemsPerPage = itemsPerPage > 0 ? itemsPerPage : 10;
    }

    /// <summary>
    /// Get page information
    /// 获取页面信息
    /// </summary>
    public PageInfo GetPageInfo(int currentPage, int totalItems)
    {
        if (currentPage < 1)
            currentPage = 1;

        var totalPages = (int)Math.Ceiling((double)totalItems / _itemsPerPage);
        
        if (currentPage > totalPages && totalPages > 0)
            currentPage = totalPages;

        return new PageInfo
        {
            CurrentPage = currentPage,
            TotalPages = totalPages,
            TotalItems = totalItems,
            ItemsPerPage = _itemsPerPage
        };
    }

    /// <summary>
    /// Get paginated items from collection
    /// 从集合中获取分页项
    /// </summary>
    public IEnumerable<T> GetPagedItems<T>(IEnumerable<T> items, int currentPage)
    {
        if (currentPage < 1)
            currentPage = 1;

        var skip = (currentPage - 1) * _itemsPerPage;
        return items.Skip(skip).Take(_itemsPerPage);
    }

    /// <summary>
    /// Get page range for pagination controls
    /// 获取分页控件的页码范围
    /// </summary>
    public List<int> GetPageRange(int currentPage, int totalPages, int rangeSize = 5)
    {
        var pages = new List<int>();
        var startPage = Math.Max(1, currentPage - rangeSize / 2);
        var endPage = Math.Min(totalPages, startPage + rangeSize - 1);

        if (endPage - startPage < rangeSize - 1)
            startPage = Math.Max(1, endPage - rangeSize + 1);

        for (int i = startPage; i <= endPage; i++)
            pages.Add(i);

        return pages;
    }
}
