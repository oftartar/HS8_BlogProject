using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HS8_BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IConfiguration _configuration;

        public AccountController(IAppUserService appUserService, IConfiguration configuration)
        {
            _appUserService = appUserService;
            _configuration = configuration;
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
            var claims = await _appUserService.Login(model);
            if (claims is not null)
            {
                var token = GetToken(claims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
                return Unauthorized();
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
            if (!result.Succeeded)
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

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
