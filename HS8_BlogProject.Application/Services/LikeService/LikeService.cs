using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.LikeDTOs;
using HS8_BlogProject.Application.Models.VMs.AppUserVMs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Enums;
using HS8_BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public LikeService(ILikeRepository likeRepository, IAppUserRepository appUserRepository, IPostRepository postRepository, IMapper mapper)
        {
            _likeRepository = likeRepository;
            _appUserRepository = appUserRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task Create(CreateLikeDTO model)
        {
            if(model != null)
            {
                Like like = _mapper.Map<Like>(model);
                await _likeRepository.Create(like);
            }
        }

        public async Task<CreateLikeDTO> CreateLike()
        {
            CreateLikeDTO model = new CreateLikeDTO()
            {
                AppUsers = await _appUserRepository.GetFilteredList(
                    select: x => new AppUserVM
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Email = x.Email,
                        ImagePath = x.ImagePath,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.UserName)),

                Posts = await _postRepository.GetFilteredList(
                    select: x => new PostVM
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Content = x.Content,
                        ImagePath = x.ImagePath,
                        GenreName = x.Genre.Name,
                        AuthorFirstName = x.Author.FirstName,
                        AuthorLastName = x.Author.LastName,
                        GenreId = x.Genre.Id,
                        AuthorId = x.Author.Id,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Title),
                    include: x => x.Include(x => x.Genre).Include(x => x.Author))
            };

            return model;
        }

        public async Task Delete(int id)
        {
            Like like = await _likeRepository.GetDefault(x => x.Id == id);

            if (like != null)
            {
                like.DeleteDate = DateTime.Now;
                like.Status = Status.Passive;

                await _likeRepository.Delete(like);
            }
        }

        public async Task<UpdateLikeDTO> GetById(int id)
        {
            Like like = await _likeRepository.GetDefault(x => x.Id == id);

            if (like != null)
            {
                var model = _mapper.Map<UpdateLikeDTO>(like);

                model.AppUsers = await _appUserRepository.GetFilteredList(
                    select: x => new AppUserVM
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Email = x.Email,
                        ImagePath = x.ImagePath,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.UserName));

                model.Posts = await _postRepository.GetFilteredList(
                    select: x => new PostVM
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Content = x.Content,
                        ImagePath = x.ImagePath,
                        GenreName = x.Genre.Name,
                        AuthorFirstName = x.Author.FirstName,
                        AuthorLastName = x.Author.LastName,
                        GenreId = x.Genre.Id,
                        AuthorId = x.Author.Id,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Title),
                    include: x => x.Include(x => x.Genre).Include(x => x.Author));

                return model;
            }
            return null;
        }

        public async Task<List<LikeVM>> GetLikes()
        {
            var likes = await _likeRepository.GetFilteredList(
                select: x => new LikeVM
                {
                    Id = x.Id,
                    AppUserName = x.AppUser.UserName,
                    AppUserId = x.AppUser.Id,
                    PostTitle = x.Post.Title,
                    PostId = x.PostId
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Id),
                include: x => x.Include(x => x.AppUser).Include(x => x.Post)
                );

            return likes;
        }

        public async Task Update(UpdateLikeDTO model)
        {
            if (model != null)
            {
                var like = _mapper.Map<Like>(model);

                await _likeRepository.Update(like);
            }
        }
    }
}
