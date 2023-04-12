using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.VMs.PostVMs
{
    public class PostDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";
        public string AuthorImagePath { get; set; }
        public string GenreName { get; set; }
        public List<CommentVM> Comments { get; set; }
        public List<LikeVM> Likes { get; set; }
    }
}
