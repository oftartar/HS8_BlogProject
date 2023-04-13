using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(ControllerRepository.ApiHttpGet<List<CommentVM>>("Comment/GetComments"));
        }

        public async Task<IActionResult> Create()
        {
            return View(ControllerRepository.ApiHttpGet<CreateCommentDTO>("Comment/CreateComment"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDTO model)
        {
            if (ModelState.IsValid)
            {
                ControllerRepository.ApiHttpPost<CreateCommentDTO>("Comment/Create", model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            UpdateCommentDTO model = ControllerRepository.ApiHttpGet<UpdateCommentDTO>("Comment/GetById/" + id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentDTO model)
        {
            if (ModelState.IsValid)
            {
                ControllerRepository.ApiHttpPut<UpdateCommentDTO>("Comment/Update", model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ControllerRepository.ApiHttpDelete("Comment/Delete/" + id);

            return RedirectToAction("Index");
        }
    }
}
