using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.VMs.CommentVMs
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AppUserName { get; set; }
        public string PostTitle { get; set; }
        public string AppUserId { get; set; }
        public int PostId { get; set; }
    }
}
