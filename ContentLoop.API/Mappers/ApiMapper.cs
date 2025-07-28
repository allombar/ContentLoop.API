using ContentLoop.API.Dto.Article.Comments.Get;
using ContentLoop.API.Dto.Article.Get;
using ContentLoop.API.Dto.Article.Post;
using ContentLoop.API.Dto.Article.Utils;
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

        public static PagedResultModelDto<ArticlePreviewDto> ToDto(this PagedResultModel<ArticleModel> paged)
        {
            return new PagedResultModelDto<ArticlePreviewDto>
            {
                Items = paged.Items.Select(article => article.ToPreviewDto())
                .ToList(),
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };
        }

        public static ArticlePreviewDto ToPreviewDto(this ArticleModel article)
        {
            return new ArticlePreviewDto
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                AuthorName = article.AuthorName,
                CreatedAt = article.CreatedAt,
                ViewsCount = article.ViewsCount
            };
        }

        public static PagedResultModelDto<ArticleCommentDto> ToDto(this PagedResultModel<ArticleCommentsModel> paged)
        {
            return new PagedResultModelDto<ArticleCommentDto>
            {
                Items = paged.Items.Select(comment => comment.ToDto())
                .ToList(),
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };
        }

        public static ArticleCommentDto ToDto(this ArticleCommentsModel comment)
        {
            return new ArticleCommentDto
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                Content = comment.Content,
                AuthorName = comment.AuthorName,
                CreatedAt = comment.CreatedAt
            };
        }

        public static ArticleDto ToDto(this ArticleModel article)
        {
            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Description = article.Description,
                AuthorName = article.AuthorName,
                CreatedAt = article.CreatedAt,
                ViewsCount = article.ViewsCount
            };
        }

        public static CreateArticleModel ToBll(this CreateArticleDto dto)
        {
            return new CreateArticleModel
            {
                Title = dto.Title,
                Description = dto.Description,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                ViewsCount = 0
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
