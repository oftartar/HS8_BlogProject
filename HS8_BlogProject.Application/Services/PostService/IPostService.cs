using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;

namespace HS8_BlogProject.Application.Services.PostService
{
    public interface IPostService
    {
        Task Create(CreatePostDTO model);
        Task<CreatePostDTO> CreatePost();
        Task Update(UpdatePostDTO model);
        Task Delete(int id);
        Task<UpdatePostDTO> GetById(int id);
        Task<List<PostVM>> GetPosts();
        Task<PostDetailsVM> GetPostDetails(int id);
        Task<List<PostDetailsVM>> GetPostsForMember();
    }
}
