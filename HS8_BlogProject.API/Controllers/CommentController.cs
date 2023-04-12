using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Services.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetComments();

            if (comments is not null)
                return Ok(comments);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetById(id);

            if (comment is not null)
                return Ok(comment);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CreateComment()
        {
            var comment = await _commentService.CreateComment();

            if (comment is not null)
                return Ok(comment);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateCommentDTO comment)
        {
            await _commentService.Create(comment);
            return Ok(comment);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentDTO comment)
        {
            await _commentService.Update(comment);
            return Ok(comment);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.Delete(id);
            return Ok("Genre Silindi");
        }
    }
}
