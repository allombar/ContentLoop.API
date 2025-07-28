using ContentLoop.API.Dto.Article.Comments.Post;
using ContentLoop.API.Dto.Article.Get;
using ContentLoop.API.Dto.Article.Patch;
using ContentLoop.API.Dto.Article.Post;
using ContentLoop.API.Mappers;
using ContentLoop.API.Services;
using ContentLoop.BLL.Interfaces;
using ContentLoop.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ContentLoop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        public readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET /api/articles
        [HttpGet]
        public async Task<IActionResult> GetAllArticles([FromQuery] int offset = 1, [FromQuery] int limit = 5)
        {
            var result = await _articleService.GetPaginedArticlesAsync(offset, limit);
            return Ok(result.ToDto());
        }

        // GET /api/articles/author
        [Authorize]
        [HttpGet("Author")]
        public async Task<IActionResult> GetAllArticlesByAuthorId([FromQuery] int offset = 1, [FromQuery] int limit = 5)
        {
            string userIdClaim = UserContextHelper.GetUserId(User);

            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized();

            var result = await _articleService.GetPaginedArticlesByAuthorIdAsync(offset, limit, userId);
            return Ok(result.ToDto());
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] Guid id, UpdateArticleDto dto)
        {
            string userIdClaim = UserContextHelper.GetUserId(User);
           
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized();

            var model = new UpdateArticleModel
            {
                Id = id,                    
                Title = dto.Title,          
                Description = dto.Description,
                Content = dto.Content
            };

            await _articleService.UpdateArticleAsync(model, userId);
            return Ok();
        }




        // GET /api/articles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var result = await _articleService.GetArticleById(id);
            return Ok(result.ToDto());
        }

        // DELETE /api/articles/{id}
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            try
            {
                string userIdClaim = UserContextHelper.GetUserId(User);

                if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
                    return Unauthorized();

                await _articleService.DeleteArticleAsync(id, userId);

                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errors = new[] { ex.Message } });
            }
        }

        // GET /api/articles/{id}/comments
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsForArticle([FromRoute] Guid id, [FromQuery] int offset = 1, [FromQuery] int limit = 10)
        {
            var result = await _articleService.GetPaginedCommentsByArticleId(id, offset, limit);
            return Ok(result.ToDto());
        }

        // POST /api/articles/{id}/comments
        [Authorize]
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddCommentToArticle([FromRoute] Guid id, [FromBody] CreateCommentDto dto)
        {
            string userIdClaim = UserContextHelper.GetUserId(User);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized();
            
            //TODO finish the route
            
            return Ok();
        }

        // POST /api/articles
        [Authorize]
        [EnableRateLimiting("CreateArticlePolicy")]
        [HttpPost()]
        public async Task<IActionResult> AddArticle([FromBody] CreateArticleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string userIdClaim = UserContextHelper.GetUserId(User);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized();

            var articleModel = dto.ToBll();
            await _articleService.AddArticleAsync(articleModel, userId);
            return Ok();
        }

        //// DELETE /api/articles/{articleId}/comments/{commentId}
        //[HttpDelete("{articleId}/comments/{commentId}")]
        //public Task<IActionResult> DeleteComment(Guid articleId, Guid commentId)
        //{

        //}
    }
}
