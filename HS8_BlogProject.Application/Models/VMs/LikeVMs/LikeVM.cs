using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.VMs.LikeVMs
{
    public class LikeVM
    {
        public int Id { get; set; }
        public string AppUserName { get; set; }
        public string PostTitle { get; set; }
        public string AppUserId { get; set; }
        public int PostId { get; set; }
    }
}
