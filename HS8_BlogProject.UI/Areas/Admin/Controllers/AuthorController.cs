using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(ControllerRepository.ApiHttpGet<List<AuthorVM>>("Author/GetAuthors"));
        }

        public async Task<IActionResult> Create()
        {
            return View(ControllerRepository.ApiHttpGet<CreateAuthorDTO>("Author/CreateAuthor"));
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

                ControllerRepository.ApiHttpPost<CreateAuthorDTO>("Author/Create", model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            UpdateAuthorDTO model = ControllerRepository.ApiHttpGet<UpdateAuthorDTO>("Author/GetById/" + id);
            return View(model);
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

                ControllerRepository.ApiHttpPut<UpdateAuthorDTO>("Author/Update", model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ControllerRepository.ApiHttpDelete("Author/Delete/" + id);

            return RedirectToAction("Index");
        }
    }
}
