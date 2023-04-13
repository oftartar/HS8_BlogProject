using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using HS8_BlogProject.Domain.Entities;

namespace HS8_BlogProject.Application.AutoMapper
{
    public class Mapping : Profile
	{
        public Mapping()
		{

			CreateMap<Genre, CreateGenreDTO>().ReverseMap();
			CreateMap<Genre, UpdateGenreDTO>().ReverseMap();

			CreateMap<Author, CreateAuthorDTO>().ReverseMap();
			CreateMap<Author, UpdateAuthorDTO>().ReverseMap();

			CreateMap<Post, CreatePostDTO>().ReverseMap();
			CreateMap<Post, UpdatePostDTO>().ReverseMap();

			CreateMap<Like, CreateLikeDTO>().ReverseMap();
			CreateMap<Like, UpdateLikeDTO>().ReverseMap();

			CreateMap<Comment, CreateCommentDTO>().ReverseMap();
			CreateMap<Comment, UpdateCommentDTO>().ReverseMap();

            CreateMap<Like, LikeVM>().ReverseMap();
            CreateMap<Comment, CommentVM>().ReverseMap();

            CreateMap<AppUser, RegisterDTO>().ReverseMap();
		}
    }
}
