using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Services.AuthorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAuthors();

            if (authors is not null)
                return Ok(authors);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetById(id);

            if (author is not null)
                return Ok(author);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CreateAuthor()
        {
            var author = await _authorService.CreateAuthor();

            if (author is not null)
                return Ok(author);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDTO author)
        {
            await _authorService.Create(author);
            return Ok(author);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateAuthorDTO author)
        {
            await _authorService.Update(author);
            return Ok(author);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            return Ok("Genre Silindi");
        }
    }
}
