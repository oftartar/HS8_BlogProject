using HS8_BlogProject.Application.Extensions;
using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HS8_BlogProject.Application.Models.DTOs.AppUserDTOs
{
    public class UpdateProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
    }
}
