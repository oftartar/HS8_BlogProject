using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Services.LikeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetLikes()
        {
            var likes = await _likeService.GetLikes();

            if (likes is not null)
                return Ok(likes);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var like = await _likeService.GetById(id);

            if (like is not null)
                return Ok(like);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CreateLike()
        {
            var like = await _likeService.CreateLike();

            if (like is not null)
                return Ok(like);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateLikeDTO like)
        {
            await _likeService.Create(like);
            return Ok(like);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateLikeDTO like)
        {
            await _likeService.Update(like);
            return Ok(like);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _likeService.Delete(id);
            return Ok("Genre Silindi");
        }
    }
}
