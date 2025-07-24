using ContentLoop.DAL.Entities;
using ContentLoop.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ContentLoopDbContext _context;
        public  ArticleRepository(ContentLoopDbContext context)
        {
            _context = context;
        }

        public async Task<(List<ArticleEntity> Items, int TotalCount)> GetPagedArticlesAsync(int offset, int limit)
        {
            IQueryable<ArticleEntity> query = _context.Articles.AsQueryable();

            int totalCount = await query.CountAsync();

            List<ArticleEntity> items = await query
                .Include(a => a.Author)
                .OrderByDescending(a => a.CreatedAt)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
