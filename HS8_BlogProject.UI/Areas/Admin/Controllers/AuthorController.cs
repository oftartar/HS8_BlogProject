using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var getData = ControllerRepository.ApiHttpGet<List<AuthorVM>>("Author/GetAuthors", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<AuthorVM>>(results);

                return View(model);
            }
            return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
        }

        public async Task<IActionResult> Create()
        {
            var getData = ControllerRepository.ApiHttpGet<CreateAuthorDTO>("Author/CreateAuthor", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<CreateAuthorDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {

                if (model.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    // Resize
                    image.Mutate(x => x.Resize(600, 560));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");

                    model.ImagePath = $"/images/{guid}.jpg";
                    model.UploadPath = null;
                }

                ControllerRepository.ApiHttpPost<CreateAuthorDTO>("Author/Create", model, Request.Cookies["X-Access-Token"]);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var getData = ControllerRepository.ApiHttpGet<UpdateAuthorDTO>("Author/GetById/" + id, Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<UpdateAuthorDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path, id });
		}

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {

                if (model.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    // Resize
                    image.Mutate(x => x.Resize(600, 560));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");

                    model.ImagePath = $"/images/{guid}.jpg";
                    model.UploadPath = null;
                }

                ControllerRepository.ApiHttpPut<UpdateAuthorDTO>("Author/Update", model, Request.Cookies["X-Access-Token"]);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ControllerRepository.ApiHttpDelete("Author/Delete/" + id, Request.Cookies["X-Access-Token"]);

            return RedirectToAction("Index");
        }
    }
}
