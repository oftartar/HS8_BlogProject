using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PostController : Controller
	{
		public async Task<IActionResult> Index()
        {
            var getData = ControllerRepository.ApiHttpGet<List<PostVM>>("Post/GetPosts", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<PostVM>>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

		public async Task<IActionResult> Create()
        {
            var getData = ControllerRepository.ApiHttpGet<CreatePostDTO>("Post/CreatePost", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<CreatePostDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreatePostDTO model)
		{
			if (ModelState.IsValid)
            {

                if (model.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    image.Mutate(x => x.Resize(600, 560));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");

                    model.ImagePath = $"/images/{guid}.jpg";
                    model.UploadPath = null;
                }

                ControllerRepository.ApiHttpPost<CreatePostDTO>("Post/Create", model, Request.Cookies["X-Access-Token"]);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Create");
		}

		public async Task<IActionResult> Edit(int id)
		{
            var getData = ControllerRepository.ApiHttpGet<UpdatePostDTO>("Post/GetById/" + id, Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<UpdatePostDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path, id });
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdatePostDTO model)
		{
			if (ModelState.IsValid)
            {

                if (model.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    image.Mutate(x => x.Resize(600, 560));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");

                    model.ImagePath = $"/images/{guid}.jpg";
                    model.UploadPath = null;
                }

                ControllerRepository.ApiHttpPut<UpdatePostDTO>("Post/Update", model, Request.Cookies["X-Access-Token"]);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Edit");
		}

		public async Task<IActionResult> Details(int id)
		{
            var getData = ControllerRepository.ApiHttpGet<PostDetailsVM>("Post/GetPostDetails/" + id, Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<PostDetailsVM>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path, id });
		}

		public async Task<IActionResult> Delete(int id)
		{
			ControllerRepository.ApiHttpDelete("Post/Delete/" + id, Request.Cookies["X-Access-Token"]);

			return RedirectToAction("Index");
		}
	}
}
