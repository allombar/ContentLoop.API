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
    }
}
