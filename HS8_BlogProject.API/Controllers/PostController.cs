using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            if (posts is not null)
                return Ok(posts);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _postService.GetById(id);

            if (post is not null)
                return Ok(post);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CreatePost()
        {
            var post = await _postService.CreatePost();

            if (post is not null)
                return Ok(post);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] CreatePostDTO post)
        {
            await _postService.Create(post);
            return Ok(post);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdatePostDTO post)
        {
            await _postService.Update(post);
            return Ok(post);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return Ok("Genre Silindi");
        }

        [HttpGet]
        [Route("[action]/{id}")]
		public async Task<IActionResult> GetPostDetails(int id)
		{
			var post = await _postService.GetPostDetails(id);

			if (post is not null)
				return Ok(post);
			else
				return BadRequest();
		}

		[HttpGet]
		[Route("[action]")]
        [AllowAnonymous]
		public async Task<IActionResult> GetPostsForMember()
		{
			var posts = await _postService.GetPostsForMember();

			if (posts is not null)
				return Ok(posts);
			else
				return BadRequest();
		}
	}
}
