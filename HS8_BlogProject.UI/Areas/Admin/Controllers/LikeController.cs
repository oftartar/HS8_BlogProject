using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LikeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var getData = ControllerRepository.ApiHttpGet<List<LikeVM>>("Like/GetLikes", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<LikeVM>>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

        public async Task<IActionResult> Create()
        {
            var getData = ControllerRepository.ApiHttpGet<CreateLikeDTO>("Like/CreateLike", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<CreateLikeDTO>(results);

                return View(model);
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateLikeDTO model)
        {
            if (ModelState.IsValid)
            {
                ControllerRepository.ApiHttpPost<CreateLikeDTO>("Like/Create", model, Request.Cookies["X-Access-Token"]);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ControllerRepository.ApiHttpDelete("Like/Delete/" + id, Request.Cookies["X-Access-Token"]);

            return RedirectToAction("Index");
        }
    }
}
