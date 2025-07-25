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

        public async Task<ArticleEntity?> GetArticleByIdAsync(Guid Id)
        {
            ArticleEntity? articleEntity = await _context.Articles.Where((user) => user.Id == Id).SingleOrDefaultAsync();

            if (articleEntity is not null)
            {
                return articleEntity;
            }
            return null;
        }

        public async Task<ArticleCommentsEntity?> AddComments(ArticleCommentsEntity comments)
        {
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();

            return await _context.Comments.FindAsync(comments.Id);
        }

        public async Task<List<ArticleCommentsEntity>> GetCommentsByArticleId(Guid id)
        {
            IQueryable<ArticleCommentsEntity> query = _context.Comments.AsQueryable();

            List<ArticleCommentsEntity> items = await query
                .Where((c) => c.Article.Id == id)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return items;
        }
    }
}
