using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace HS8_BlogProject.UI.Areas.Admin.Controllers
{
	public class DashboardController : Controller
	{
		[Area("Admin")]
		public IActionResult Index()
        {
			if (Request.Cookies["X-Access-Token"] is not null)
			{
				return View();
			}
			return RedirectToAction("Login", "Account", new { area = "", returnUrl = Request.Path });
		}
	}
}
