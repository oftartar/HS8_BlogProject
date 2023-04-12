using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GenreController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View(ControllerRepository.ApiHttpGet<List<GenreVM>>("Genre/GetGenres"));
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
				ControllerRepository.ApiHttpPost<CreateGenreDTO>("Genre/Create", model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Edit(int id)
		{
			UpdateGenreDTO model = ControllerRepository.ApiHttpGet<UpdateGenreDTO>("Genre/GetById/" + id);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateGenreDTO model)
		{
			if (ModelState.IsValid)
			{
				ControllerRepository.ApiHttpPut<UpdateGenreDTO>("Genre/Update", model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			ControllerRepository.ApiHttpDelete("Genre/Delete/" + id);

			return RedirectToAction("Index");
		}
	}
}
