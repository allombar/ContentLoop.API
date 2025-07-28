using ContentLoop.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.BLL.Interfaces
{
    public interface IArticleService
    {
        public Task<PagedResultModel<ArticleModel>> GetPaginedArticlesAsync(int offset, int? limit);
        public Task<PagedResultModel<ArticleModel>> GetPaginedArticlesByAuthorIdAsync(int offset, int? limit, Guid authorId);
        public Task<ArticleModel> GetArticleById(Guid id);
        public Task UpdateArticleAsync(UpdateArticleModel article, Guid currentUserId);
        public Task<PagedResultModel<ArticleCommentsModel>> GetPaginedCommentsByArticleId(Guid id, int offset, int? limit);
        public Task DeleteArticleAsync(Guid articleId, Guid currentUserId);
        public Task AddArticleAsync(CreateArticleModel article, Guid authorId);
    }
}
