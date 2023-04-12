using HS8_BlogProject.Application.Models.VMs.AppUserVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.DTOs.LikeDTOs
{
    public class CreateLikeDTO
    {
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public string AppUserId { get; set; }
        public string PostId { get; set; }
        public List<AppUserVM>? AppUsers { get; set; }
        public List<PostVM>? Posts { get; set; }
    }
}
