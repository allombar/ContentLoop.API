using ContentLoop.DAL.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.DAL.Interfaces
{
    public interface IArticleRepository
    {
        public Task<(List<ArticleEntity> Items, int TotalCount)> GetPagedArticlesAsync(int offset, int limit);
        public Task<(List<ArticleEntity> Items, int TotalCount)> GetPagedArticlesByAuthorIdAsync(int offset, int limit, Guid authorId);
        public Task UpdateArticleAsync(ArticleEntity updatedArticle);
        public Task<ArticleEntity?> GetArticleByIdAsync(Guid Id);
        public Task<ArticleCommentsEntity?> AddComment(ArticleCommentsEntity comment);
        public Task<(List<ArticleCommentsEntity> Items, int TotalCount)> GetCommentsByArticleId(Guid Id, int offset, int limit);
        public Task DeleteArticleById(Guid articleId, Guid authorId);
        public Task AddArticle(ArticleEntity article);
    }
}
