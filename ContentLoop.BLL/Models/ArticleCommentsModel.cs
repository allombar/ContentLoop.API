namespace ContentLoop.BLL.Models
{
    public class ArticleCommentsModel
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
