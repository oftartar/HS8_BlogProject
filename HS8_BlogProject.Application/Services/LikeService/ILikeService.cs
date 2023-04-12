using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Services.LikeService
{
	public interface ILikeService
    {
        Task Create(CreateLikeDTO model);
        Task<CreateLikeDTO> CreateLike();
        Task Update(UpdateLikeDTO model);
        Task Delete(int id);
        Task<UpdateLikeDTO> GetById(int id);
        Task<List<LikeVM>> GetLikes();
    }
}
