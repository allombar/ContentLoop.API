namespace ContentLoop.API.Dto.Article.Get
{
    public class ArticlePreviewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewsCount { get; set; }
    }
}
