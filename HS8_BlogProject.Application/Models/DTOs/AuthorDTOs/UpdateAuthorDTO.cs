using HS8_BlogProject.Application.Extensions;
using HS8_BlogProject.Application.Models.VMs.AppUserVMs;
using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HS8_BlogProject.Application.Models.DTOs.AuthorDTOs
{
    public class UpdateAuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public string AppUserId { get; set; }
        public List<AppUserVM>? AppUsers { get; set; }
    }
}
