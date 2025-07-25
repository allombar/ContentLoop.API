namespace ContentLoop.API.Dto.Article.Get
{
    public class PagedModelDto<ArticleDto>
    {
        public List<ArticleDto> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
