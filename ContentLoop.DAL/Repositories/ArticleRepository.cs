using ContentLoop.DAL.Entities;
using ContentLoop.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        public ArticleRepository(ContentLoopDbContext context)
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

        public async Task UpdateArticleAsync(ArticleEntity updatedArticle)
        {
            _context.Articles.Update(updatedArticle);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<ArticleEntity> Items, int TotalCount)> GetPagedArticlesByAuthorIdAsync(int offset, int limit, Guid authorId)
        {
            IQueryable<ArticleEntity> query = _context.Articles.AsQueryable();

            var baseQuery = _context.Articles.Where(a => a.AuthorId == authorId);
            int totalCount = await baseQuery.CountAsync();

            List<ArticleEntity> items = await baseQuery
                .Include(a => a.Author)
                .Where(a => a.Author.Id == authorId)
                .OrderByDescending(a => a.CreatedAt)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<ArticleEntity?> GetArticleByIdAsync(Guid Id)
        {
            ArticleEntity? articleEntity = await _context.Articles
                .Include(a => a.Author)
                .Where((user) => user.Id == Id)
                .FirstAsync();

            if (articleEntity is not null)
            {
                articleEntity.ViewsCount++; 
                await _context.SaveChangesAsync();

                return articleEntity;
            }
            return null;
        }

        public async Task<ArticleCommentsEntity?> AddComment(ArticleCommentsEntity comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return await _context.Comments.FindAsync(comment.Id);
        }

        public async Task AddArticle(ArticleEntity article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<ArticleCommentsEntity> Items, int TotalCount)> GetCommentsByArticleId(Guid id, int offset, int limit)
        {
            IQueryable<ArticleCommentsEntity> query = _context.Comments.AsQueryable();

            var baseQuery = _context.Comments.Where(c => c.Article.Id == id);
            int totalCount = await baseQuery.CountAsync();

            List<ArticleCommentsEntity> items = await baseQuery
                .Include(c => c.Author)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task DeleteArticleById(Guid articleId, Guid authorId)
        {
            await _context.Articles
            .Where(a => a.Id == articleId && a.AuthorId == authorId)
            .ExecuteDeleteAsync();
        }

        public async Task DeleteCommentByArticleId(Guid commentId, Guid authorId)
        {
            await _context.Comments
            .Where(c => c.Id == commentId && c.AuthorId == authorId)
            .ExecuteDeleteAsync();
        }
    }
}
