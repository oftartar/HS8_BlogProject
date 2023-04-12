using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Services.GenreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetGenres()
		{
			var genres = await _genreService.GetGenres();

			if (genres is not null)
				return Ok(genres);
			else
				return BadRequest();
		}

		[HttpGet]
		[Route("[action]/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var genre = await _genreService.GetById(id);

			if (genre is not null)
				return Ok(genre);
			else
				return BadRequest();
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> Create([FromBody] CreateGenreDTO genre)
		{
			await _genreService.Create(genre);
			return Ok(genre);
		}

		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> Update([FromBody] UpdateGenreDTO genre)
		{
			await _genreService.Update(genre);
			return Ok(genre);
		}

		[HttpDelete]
		[Route("[action]/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _genreService.Delete(id);
			return Ok("Genre Silindi");
		}
	}
}
