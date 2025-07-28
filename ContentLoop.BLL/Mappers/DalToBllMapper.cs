using ContentLoop.BLL.Models;
using ContentLoop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.BLL.Mappers
{
    public static class DalToBllMapper
    {
        public static UserModel ToBll(this UserEntity entity)
        {
            return new UserModel()
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Role = entity.Role
            };
        }

        public static PagedResultModel<ArticleModel> PagedResultArticleModelToBll(List<ArticleEntity> article, int totalCount, int offset, int limit)
        {

            PagedResultModel<ArticleModel> response = new();

            foreach (ArticleEntity a in article)
            {
                response.Items.Add(a.ToBllPreview()); 
            }

            response.TotalCount = totalCount;
            response.Page = offset;
            response.PageSize = limit;

            return response;
        }

        public static PagedResultModel<ArticleCommentsModel> PagedResultArticleCommentsModelToBll(List<ArticleCommentsEntity> comments, int totalCount, int offset, int limit)
        {
            PagedResultModel<ArticleCommentsModel> response = new();

            foreach (ArticleCommentsEntity c in comments)
            {
                response.Items.Add(c.ToBll());
            }

            response.TotalCount = totalCount;
            response.Page = offset;
            response.PageSize = limit;
            return response;
        }

        public static ArticleModel ToBll(this ArticleEntity entity)
        {
            return new ArticleModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                Description = entity.Description,
                AuthorName = $"{entity.Author.FirstName} {entity.Author.LastName}",
                ViewsCount = entity.ViewsCount,
                CreatedAt = entity.CreatedAt
            };
        }

        public static ArticleModel ToBllPreview(this ArticleEntity entity)
        {
            return new ArticleModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                AuthorName = $"{entity.Author.FirstName} {entity.Author.LastName}",
                ViewsCount = entity.ViewsCount,
                CreatedAt = entity.CreatedAt
            };
        }

        public static ArticleCommentsModel ToBll(this ArticleCommentsEntity entity)
        {
            return new ArticleCommentsModel()
            {
                Id = entity.Id,
                ArticleId = entity.ArticleId,
                AuthorName = $"{entity.Author.FirstName} {entity.Author.LastName}",
                Content = entity.Content,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
