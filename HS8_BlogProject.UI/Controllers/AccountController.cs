using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace HS8_BlogProject.UI.Controllers
{
    public class AccountController : Controller
    {

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
                var httpResponse = ControllerRepository.ApiHttpPost("Account/Register", registerDTO);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ClaimsPrincipal>(httpResponse.Content.ReadAsStringAsync().Result);
                    User.AddIdentity(result.Identity as ClaimsIdentity);
                    return RedirectToAction("Index", "");
                }
                else
                {
                    var errors = JsonConvert.DeserializeObject<IEnumerable<IdentityError>>(httpResponse.Content.ReadAsStringAsync().Result);
                    foreach (var item in errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                        TempData["Error"] = "Something went wrong";
                    }
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
                var httpResponse = ControllerRepository.ApiHttpPost("Account/Login", loginDTO);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ClaimsPrincipal>(httpResponse.Content.ReadAsStringAsync().Result);
                    User.AddIdentity(result.Identity as ClaimsIdentity);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                }

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
            var httpResponse = ControllerRepository.ApiHttpPost("Account/Logout",User);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(string username)
        {
            if (username != "")
            {
                var user = ControllerRepository.ApiHttpGet<UpdateProfileDTO>("Account/GetByUserName/" + username);
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
                    ControllerRepository.ApiHttpPut<UpdateProfileDTO>("Author/Update", updateProfileDTO);

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
                var user = ControllerRepository.ApiHttpGet<UpdateProfileDTO>("Account/GetByUserName/" + username);
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
