using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Application.Services.PostService;
using HS8_BlogProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HS8_BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            List<PostVM> posts = await _postService.GetPosts();

            return View(posts);
        }
    }
}