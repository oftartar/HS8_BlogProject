using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			CreateMap<AppUser, RegisterDTO>().ReverseMap();
		}
    }
}
