using ContentLoop.BLL.Interfaces;
using ContentLoop.BLL.Mappers;
using ContentLoop.BLL.Models;
using ContentLoop.DAL.Entities;
using ContentLoop.DAL.Interfaces;
namespace ContentLoop.BLL.Services
{
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

            return DalToBllMapper.PagedResultModelToBll(entities, totalCount, offset, safeLimit);
        }

        public async Task<ArticleModel?> GetArticleById(Guid id)
        {
            ArticleEntity? article = await _repository.GetArticleByIdAsync(id);

            return article.ToBll();
        }
    }
}
