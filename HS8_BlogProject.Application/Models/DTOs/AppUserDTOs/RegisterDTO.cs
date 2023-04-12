using HS8_BlogProject.Domain.Enums;

namespace HS8_BlogProject.Application.Models.DTOs.AppUserDTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; } = $"/images/defaultuser.jpg";
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
