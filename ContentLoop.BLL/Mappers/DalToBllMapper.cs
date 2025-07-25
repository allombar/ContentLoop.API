using ContentLoop.BLL.Models;
using ContentLoop.DAL.Entities;
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

        public static PagedResultModel<ArticleModel> PagedResultModelToBll(List<ArticleEntity> article, int totalCount, int offset, int limit)
        {

            PagedResultModel<ArticleModel> response = new();

            foreach (ArticleEntity a in article)
            {
                response.Items.Add(a.ToBll()); 
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
                AuthorName = $"{entity.Author.FirstName} {entity.Author.LastName}",
                ViewsCount = entity.ViewsCount,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
