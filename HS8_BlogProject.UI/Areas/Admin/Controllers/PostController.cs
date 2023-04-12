using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PostController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View(ControllerRepository.ApiHttpGet<List<PostVM>>("Post/GetPosts"));
		}

		public async Task<IActionResult> Create()
		{
			return View(ControllerRepository.ApiHttpGet<CreatePostDTO>("Post/CreatePost"));
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreatePostDTO model)
		{
			if (ModelState.IsValid)
			{
				ControllerRepository.ApiHttpPost<CreatePostDTO>("Post/Create", model);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Create");
		}

		public async Task<IActionResult> Edit(int id)
		{
			UpdatePostDTO model = ControllerRepository.ApiHttpGet<UpdatePostDTO>("Post/GetById/" + id);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdatePostDTO model)
		{
			if (ModelState.IsValid)
			{
				ControllerRepository.ApiHttpPut<UpdatePostDTO>("Post/Update", model);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Edit");
		}

		public async Task<IActionResult> Details(int id)
		{
			PostDetailsVM model = ControllerRepository.ApiHttpGet<PostDetailsVM>("Post/GetPostDetails/" + id);
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			ControllerRepository.ApiHttpDelete("Post/Delete/" + id);

			return RedirectToAction("Index");
		}
	}
}
