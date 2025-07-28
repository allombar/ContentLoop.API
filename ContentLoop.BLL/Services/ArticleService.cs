using ContentLoop.BLL.Interfaces;
using ContentLoop.BLL.Mappers;
using ContentLoop.BLL.Models;
using ContentLoop.DAL.Entities;
using ContentLoop.DAL.Interfaces;
using ContentLoop.DAL.Repositories;
namespace ContentLoop.BLL.Services
{
    //TODO: Add BLL validation
    public class ArticleService : IArticleService
    {
        public readonly IArticleRepository _repository;
        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultModel<ArticleModel>> GetPaginedArticlesAsync(int offset, int? limit)
        {
            int safeLimit = Math.Min(100, limit ?? 100);

            var (entities, totalCount) = await _repository.GetPagedArticlesAsync(offset, safeLimit);

            return DalToBllMapper.PagedResultArticleModelToBll(entities, totalCount, offset, safeLimit);
        }

        public async Task<PagedResultModel<ArticleModel>> GetPaginedArticlesByAuthorIdAsync(int offset, int? limit, Guid authorId)
        {
            int safeLimit = Math.Min(100, limit ?? 100);

            var (entities, totalCount) = await _repository.GetPagedArticlesByAuthorIdAsync(offset, safeLimit, authorId);

            return DalToBllMapper.PagedResultArticleModelToBll(entities, totalCount, offset, safeLimit);
        }

        public async Task<ArticleModel?> GetArticleById(Guid id)
        {
            ArticleEntity? article = await _repository.GetArticleByIdAsync(id);
            if (article == null)
            {
                throw new ArgumentException($"L'article {id} n'a pas été trouvé");
            }

            return article.ToBll();
        }

        public async Task<PagedResultModel<ArticleCommentsModel>> GetPaginedCommentsByArticleId(Guid id, int offset, int? limit)
        {
            int safeLimit = Math.Min(100, limit ?? 100);
            var (entities, totalCount) = await _repository.GetCommentsByArticleId(id, offset, safeLimit);
            return DalToBllMapper.PagedResultArticleCommentsModelToBll(entities, totalCount, offset, safeLimit);
        }

        public async Task AddArticleAsync(CreateArticleModel article, Guid authorId)
        {

            var articleEntity = article.ToEntity();
            articleEntity.AuthorId = authorId;
            await _repository.AddArticle(articleEntity);
        }

        public async Task UpdateArticleAsync(UpdateArticleModel article, Guid currentUserId)
        {
            var existingArticle = await _repository.GetArticleByIdAsync(article.Id);
            if (existingArticle == null)
            {
                throw new ArgumentException($"L'article {article.Id} n'a pas été trouvé");
            }

            if (existingArticle.AuthorId != currentUserId)
            {
                throw new UnauthorizedAccessException("Vous ne pouvez modifier que vos articles");
            }

            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.Description = article.Description;
            existingArticle.UpdatedAt = DateTime.UtcNow; 


            await _repository.UpdateArticleAsync(existingArticle);
        }

        public async Task DeleteArticleAsync(Guid articleId, Guid currentUserId)
        {

            var article = await _repository.GetArticleByIdAsync(articleId);
            if (article == null)
            {
                throw new ArgumentException($"L'article {articleId} n'a pas été trouvé");
            }

            if (article.AuthorId != currentUserId)
            {
                throw new ArgumentException("Vous ne pouvez supprimer que vos articles");
            }

            await _repository.DeleteArticleById(articleId, currentUserId);
        }
    }
}
