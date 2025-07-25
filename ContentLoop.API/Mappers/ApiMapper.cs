using ContentLoop.API.Dto.Article.Get;
using ContentLoop.API.Dto.Auth.Get;
using ContentLoop.API.Dto.Auth.Post;
using ContentLoop.BLL.Models;

namespace ContentLoop.API.Mappers
{
    public static class ApiMapper
    {
        public static UserDto ToDto(this UserModel user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }

        public static PagedModelDto<ArticleDto> ToDto(this PagedResultModel<ArticleModel> paged)
        {
            return new PagedModelDto<ArticleDto>
            {
                Items = paged.Items.Select(article => article.ToDto())
                .ToList(),
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };
        }

        public static ArticleDto ToDto(this ArticleModel article)
        {
            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                AuthorName = article.AuthorName,
                CreatedAt = article.CreatedAt,
                ViewsCount = article.ViewsCount
            };
        }

        public static UserModel ToBll(this RegisterDto dto)
        {
            return new UserModel
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                CreatedAt = DateTime.Now
            };
        }
    }
}
