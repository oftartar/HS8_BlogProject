using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS8_BlogProject.Domain.Entities
{
    public class Comment : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        //IBaseEntity Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Property
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
