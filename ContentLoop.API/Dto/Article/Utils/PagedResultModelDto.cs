using ContentLoop.API.Dto.Article.Get;

namespace ContentLoop.API.Dto.Article.Utils
{
    public class PagedResultModelDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
