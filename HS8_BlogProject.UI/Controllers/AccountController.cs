using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HS8_BlogProject.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            if (Request.Cookies["username"] is not null)
            {
                return RedirectToAction("Index", "");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var httpResponse = ControllerRepository.ApiHttpPost("Account/Register", registerDTO, Request.Cookies["X-Access-Token"]);
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

        public IActionResult Login(string returnUrl = "/")
        {
            if (Request.Cookies["username"] is not null)
            {
                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }

            ViewData["ReturnURL"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var httpResponse = ControllerRepository.ApiHttpPost("Account/Login", loginDTO, Request.Cookies["X-Access-Token"]);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var jwtToken = JsonConvert.DeserializeObject<String>(httpResponse.Content.ReadAsStringAsync().Result);
                    Response.Cookies.Append("X-Access-Token", jwtToken,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict
                        });
                    Response.Cookies.Append("username",
                        new JwtSecurityTokenHandler().ReadJwtToken(jwtToken).Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict
                        });
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
            if (Request.Cookies["username"] != null)
            {
                var httpResponse = ControllerRepository.ApiHttpPost("Account/Logout", User, Request.Cookies["X-Access-Token"]);
                Response.Cookies.Delete("username");
                Response.Cookies.Delete("X-Access-Token");

            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(string username)
        {
            if (username != "")
            {
                var getData = ControllerRepository.ApiHttpGet<UpdateProfileDTO>("Account/GetByUserName/" + username, Request.Cookies["X-Access-Token"]);

                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<UpdateProfileDTO>(results);

                return View(model);
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
                    ControllerRepository.ApiHttpPut<UpdateProfileDTO>("Author/Update", updateProfileDTO, Request.Cookies["X-Access-Token"]);

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
                var getData = ControllerRepository.ApiHttpGet<UpdateProfileDTO>("Account/GetByUserName/" + username, Request.Cookies["X-Access-Token"]);

                string results = getData.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<UpdateProfileDTO>(results);

                return View(model);
            }
            else
            {
                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }
        }
    }
}
