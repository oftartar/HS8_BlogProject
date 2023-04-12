using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS8_BlogProject.Domain.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }

        //IBaseEntity Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation Property
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public Author Author { get; set; }

        public AppUser()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}
