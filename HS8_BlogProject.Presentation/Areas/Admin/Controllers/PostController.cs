using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	public class PostController : Controller
	{
		private readonly IPostService _postService;

		public PostController(IPostService postService)
		{
			_postService = postService;
		}

		public async Task<IActionResult> Index()
		{
			List<PostVM> posts = await _postService.GetPosts();
			return View(posts);
		}

		public async Task<IActionResult> Create()
		{
			CreatePostDTO model = await _postService.CreatePost();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreatePostDTO model)
		{
            if (ModelState.IsValid)
			{
				await _postService.Create(model);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Create");
		}

		public async Task<IActionResult> Edit(int id)
		{
			UpdatePostDTO model = await _postService.GetById(id);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdatePostDTO model)
		{
			if (ModelState.IsValid)
			{
				await _postService.Update(model);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Edit");
		}

		public async Task<IActionResult> Details(int id)
		{
			PostDetailsVM model = await _postService.GetPostDetails(id);
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			await _postService.Delete(id);

			return RedirectToAction("Index");
		}
	}
}
