using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LikeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(ControllerRepository.ApiHttpGet<List<LikeVM>>("Like/GetLikes"));
        }

        public async Task<IActionResult> Create()
        {
            return View(ControllerRepository.ApiHttpGet<CreateLikeDTO>("Like/CreateLike"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLikeDTO model)
        {
            if (ModelState.IsValid)
            {
                ControllerRepository.ApiHttpPost<CreateLikeDTO>("Like/Create", model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ControllerRepository.ApiHttpDelete("Like/Delete/" + id);

            return RedirectToAction("Index");
        }
    }
}
