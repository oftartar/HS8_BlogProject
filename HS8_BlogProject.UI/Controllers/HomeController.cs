using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.UI.Controllers
{
    public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View(ControllerRepository.ApiHttpGet<List<PostDetailsVM>>("Post/GetPostsForMember"));
		}
	}
}