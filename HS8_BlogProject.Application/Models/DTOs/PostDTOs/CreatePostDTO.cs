using HS8_BlogProject.Application.Extensions;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HS8_BlogProject.Application.Models.DTOs.PostDTOs
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; } = $"/images/noimage.jpg";
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        // Genre ve Author CM listerleri doldurulacak
        public List<AuthorVM>? Authors { get; set; }
        public List<GenreVM>? Genres { get; set; }
    }
}
