using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GenreController : Controller
	{
		public async Task<IActionResult> Index()
		{
			var getData = ControllerRepository.ApiHttpGet<List<GenreVM>>("Genre/GetGenres", Request.Cookies["X-Access-Token"]);
            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<GenreVM>>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

		public async Task<IActionResult> Create()
		{
			if (Request.Cookies["username"] is not null)
			{
				return View(new CreateGenreDTO());
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateGenreDTO model)
		{
			if (ModelState.IsValid)
			{
				ControllerRepository.ApiHttpPost<CreateGenreDTO>("Genre/Create", model, Request.Cookies["X-Access-Token"]);
				return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

		public async Task<IActionResult> Edit(int id)
		{
			var getData = ControllerRepository.ApiHttpGet<UpdateGenreDTO>("Genre/GetById/" + id, Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<UpdateGenreDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path, id});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateGenreDTO model)
		{
			if (ModelState.IsValid)
			{
				ControllerRepository.ApiHttpPut<UpdateGenreDTO>("Genre/Update", model, Request.Cookies["X-Access-Token"]);
				return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

		public async Task<IActionResult> Delete(int id)
		{
			ControllerRepository.ApiHttpDelete("Genre/Delete/" + id, Request.Cookies["X-Access-Token"]);

			return RedirectToAction("Index");
		}
	}
}
