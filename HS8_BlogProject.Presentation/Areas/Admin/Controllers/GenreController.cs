using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Application.Services.GenreService;
using HS8_BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HS8_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	public class GenreController : Controller
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		public async Task<IActionResult> Index()
		{
			List<GenreVM> genres = await _genreService.GetGenres();
			return View(genres);
		}

		public async Task<IActionResult> Create()
		{
			return View(new CreateGenreDTO());
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateGenreDTO model)
		{
			if (ModelState.IsValid)
			{
				await _genreService.Create(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Edit(int id)
		{
			UpdateGenreDTO model = await _genreService.GetById(id);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateGenreDTO model)
		{
			if (ModelState.IsValid)
			{
				await _genreService.Update(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			await _genreService.Delete(id);

			return RedirectToAction("Index");
		}
	}
}
