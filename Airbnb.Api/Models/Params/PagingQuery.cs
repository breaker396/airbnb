using System.ComponentModel.DataAnnotations;

namespace Airbnb.Api.Models.Params
{
    public class PagingQuery
    {
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;

        [Range(1, int.MaxValue, ErrorMessage = "Page Index is positive number only")]
        public int PageIndex { get; set; } = 1;

        public string OrderBy { get; set; } = string.Empty;
        public bool OrderByDesc { get; set; } = false;
        public PagingQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public PagingQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
    public class Paging<T>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public long TotalCount { get; set; } = 0;
        public IEnumerable<T> Result { get; set; } = new List<T>();

        public Paging(int pageIndex, int pageSize, long totalCount, IEnumerable<T> result) : this(pageIndex, pageSize)
        {
            TotalCount = totalCount;
            Result = result;
        }

        public Paging(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = 0;
            Result = new List<T>();
        }

        public Paging()
        {
        }
    }
}
