using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var getData = ControllerRepository.ApiHttpGet<List<CommentVM>>("Comment/GetComments", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<CommentVM>>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

        public async Task<IActionResult> Create()
        {
            var getData = ControllerRepository.ApiHttpGet<CreateCommentDTO>("Comment/CreateComment", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<CreateCommentDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDTO model)
        {
            if (ModelState.IsValid)
            {
                ControllerRepository.ApiHttpPost<CreateCommentDTO>("Comment/Create", model, Request.Cookies["X-Access-Token"]);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var getData = ControllerRepository.ApiHttpGet<UpdateCommentDTO>("Comment/GetById/" + id, Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<UpdateCommentDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path, id });
		}

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentDTO model)
        {
            if (ModelState.IsValid)
            {
                ControllerRepository.ApiHttpPut<UpdateCommentDTO>("Comment/Update", model, Request.Cookies["X-Access-Token"]);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ControllerRepository.ApiHttpDelete("Comment/Delete/" + id, Request.Cookies["X-Access-Token"]);

            return RedirectToAction("Index");
        }
    }
}
