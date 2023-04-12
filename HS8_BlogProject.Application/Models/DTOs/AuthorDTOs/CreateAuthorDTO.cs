using HS8_BlogProject.Application.Extensions;
using HS8_BlogProject.Application.Models.VMs.AppUserVMs;
using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.DTOs.AuthorDTOs
{
    public class CreateAuthorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; } = $"/images/defaultuser.jpg";
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public string AppUserId { get; set; }
        public List<AppUserVM>? AppUsers { get; set; }
    }
}
