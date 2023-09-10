using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace HS8_BlogProject.UI.Controllers
{
    public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			var getData = ControllerRepository.ApiHttpGet<List<PostDetailsVM>>("Post/GetPostsForMember", Request.Cookies["X-Access-Token"]);

            if (getData.IsSuccessStatusCode)
            {
                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<PostDetailsVM>>(results);

                return View(model);
            }
            return View();
        }
	}
}