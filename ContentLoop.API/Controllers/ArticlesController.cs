using ContentLoop.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAllArticles([FromQuery] int offset = 1, [FromQuery] int limit = 5)
        {
            var result = await _articleService.GetPaginedArticlesAsync(offset, limit);
            return Ok(result);
        }

        //// GET /api/articles/{id}
        //[HttpGet("{id}")]
        //public Task<IActionResult> GetArticle(Guid id)
        //{

        //}

        //// GET /api/articles/{id}/comments
        //[HttpGet("{id}/comments")]
        //public Task<IActionResult> GetCommentsForArticle(Guid id)
        //{

        //}

        //// POST /api/articles/{id}/comments
        //[HttpPost("{id}/comments")]
        //public Task<IActionResult> AddCommentToArticle(Guid id, [FromBody] CreateCommentDto dto)
        //{

        //}

        //// DELETE /api/articles/{articleId}/comments/{commentId}
        //[HttpDelete("{articleId}/comments/{commentId}")]
        //public Task<IActionResult> DeleteComment(Guid articleId, Guid commentId)
        //{

        //}
    }
}
