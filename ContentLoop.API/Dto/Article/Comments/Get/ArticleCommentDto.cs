namespace ContentLoop.API.Dto.Article.Comments.Get
{
    public class ArticleCommentDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
