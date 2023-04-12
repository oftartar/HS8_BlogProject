using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Models.VMs;
using HS8_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.Presentation.Controllers
{
    [Authorize]
	public class AccountController : Controller
	{
		private readonly IAppUserService _appUserService;

		public AccountController(IAppUserService appUserService)
		{
			_appUserService = appUserService;
		}

		[AllowAnonymous]
		public IActionResult Register()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "");
			}
			return View();
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> Register(RegisterDTO registerDTO)
		{
			if (ModelState.IsValid)
			{
				var result = await _appUserService.Register(registerDTO);
				if (result.Succeeded)
					return RedirectToAction("Index", "");
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
					TempData["Error"] = "Something went wrong";
				}
			}
			return View();
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl = "/")
		{
			if (User.Identity.IsAuthenticated)
			{
				//return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
				return RedirectToAction("Index", "");
			}

			ViewData["ReturnURL"] = returnUrl;

			return View();
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> Login(LoginDTO loginDTO, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var result = await _appUserService.Login(loginDTO);
				if (result.Succeeded)
					return RedirectToLocal(returnUrl);

				ModelState.AddModelError("", "Invalid Login Attempt");

			}
			return View(loginDTO);
		}

		private IActionResult RedirectToLocal(string returnUrl = "/")
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				//return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
				return RedirectToAction("Index", "");
			}
		}

		public async Task<IActionResult> LogOut()
		{
			await _appUserService.LogOut();
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Edit(string username)
		{
			if (username != "")
			{
				UpdateProfileDTO user = await _appUserService.GetByUserName(username);
				return View(user);
			}
			else
			{
				//return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
				return RedirectToAction("Index", "");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateProfileDTO updateProfileDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _appUserService.UpdateUser(updateProfileDTO);

				}
				catch (Exception)
				{
					TempData["Error"] = "Something went wrong";
				}
				return RedirectToAction("Index", "");
			}
			return View(updateProfileDTO);
		}

		public async Task<IActionResult> ProfileDetails(string username)
		{
			if (username != "")
			{
				UpdateProfileDTO user = await _appUserService.GetByUserName(username);
				return View(user);
			}
			else
			{
				//return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
				return RedirectToAction("Index", "");
			}
		}
	}
}
