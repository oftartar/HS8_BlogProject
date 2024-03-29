﻿using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HS8_BlogProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<List<Claim>> Login(LoginDTO model);
        Task LogOut();
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateProfileDTO model);
    }
}
