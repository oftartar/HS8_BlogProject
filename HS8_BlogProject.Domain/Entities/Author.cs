using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS8_BlogProject.Domain.Entities
{
    public class Author : IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }

        //IBaseEntity Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Property
        public List<Post> Posts { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Author()
        {
            Posts = new List<Post>();
        }
    }
}
