using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Enums;
using HS8_BlogProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HS8_BlogProject.Application.Services.AppUserService
{
    internal class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    ImagePath = x.ImagePath,
                },
                where: x => x.UserName == userName);

            return result;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                var user = _mapper.Map<AppUser>(model);

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                return result;
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "Password and ConfirmPassword doesn't match."
            });
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            if (user != null)
            {
                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

                    if (model.UserName != null)
                    {
                        AppUser isUserNameExists = await _userManager.FindByNameAsync(model.UserName);
                        if (isUserNameExists == null)
                            await _userManager.SetUserNameAsync(user, model.UserName);
                    }

                    if (model.Email != null)
                    {
                        AppUser isUserEmailExists = await _userManager.FindByEmailAsync(model.Email);
                        if (isUserEmailExists == null)
                            await _userManager.SetEmailAsync(user, model.Email);
                    }

                    user.UpdateDate = DateTime.Now;
                    user.Status = Status.Modified;

                    await _userManager.UpdateAsync(user);
                    await LogOut();
                }
            }
        }
    }
}
