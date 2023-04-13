using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Mvc;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [HttpGet]
        [Route("[action]/{username}")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var user = await _appUserService.GetByUserName(username);

            if (user is not null)
                return Ok(user);
            else
                return BadRequest();
        }
        //Hocaya sor
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _appUserService.Login(model);
            if (result.Succeeded)
                return Ok(User);
            else
                return BadRequest(result);
        }
        //Hocaya sor
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO appUser)
        {
            var result = await _appUserService.Register(appUser);
            if (result.Succeeded) 
                return Ok(User);
            else
                return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateProfileDTO user)
        {
            await _appUserService.UpdateUser(user);
            return Ok(user);
        }
    }
}
