using AICMS.Core.Pagination;
using Xunit;

namespace AICMS.Tests
{
    public class PaginationTests
    {
        [Fact]
        public void GetPageInfo_Calculates_Correctly()
        {
            var handler = new PaginationHandler(itemsPerPage: 10);
            var info = handler.GetPageInfo(1, 95);

            Assert.Equal(10, info.ItemsPerPage);
            Assert.Equal(10, info.TotalPages);
            Assert.Equal(1, info.CurrentPage);
        }

        [Fact]
        public void GetPagedItems_Returns_Correct_Count()
        {
            var handler = new PaginationHandler(itemsPerPage: 10);
            var items = Enumerable.Range(1, 25).ToList();
            var page2 = handler.GetPagedItems(items, 2).ToList();

            Assert.Equal(10, page2.Count);
            Assert.Equal(11, page2.First());
        }
    }
}
