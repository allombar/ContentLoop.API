using ContentLoop.DAL.Entities;
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
        public Task<ArticleEntity?> GetArticleByIdAsync(Guid Id);
    }
}
