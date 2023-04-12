using HS8_BlogProject.Application.Extensions;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.DTOs.PostDTOs
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        // Genre ve Author CM listerleri doldurulacak
        public List<AuthorVM>? Authors { get; set; }
        public List<GenreVM>? Genres { get; set; }
    }
}
