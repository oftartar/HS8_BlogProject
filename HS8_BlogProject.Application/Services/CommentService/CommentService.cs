using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS8_BlogProject.Application.Models.VMs.AppUserVMs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
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

namespace HS8_BlogProject.Application.Services.CommentService
{
    public class CommentService : ICommentService
	{
        private readonly ICommentRepository _commentRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IAppUserRepository appUserRepository, IPostRepository postRepository, IMapper mapper)
		{
            _commentRepository = commentRepository;
            _appUserRepository = appUserRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }
		public async Task Create(CreateCommentDTO model)
		{
			if (model != null)
			{
				Comment comment = _mapper.Map<Comment>(model);
				await _commentRepository.Create(comment);
			}
		}

		public async Task<CreateCommentDTO> CreateComment()
		{
            CreateCommentDTO model = new CreateCommentDTO()
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
            Comment comment = await _commentRepository.GetDefault(x => x.Id == id);

            if (comment != null)
            {
                comment.DeleteDate = DateTime.Now;
                comment.Status = Status.Passive;

                await _commentRepository.Delete(comment);
            }
        }

		public async Task<UpdateCommentDTO> GetById(int id)
		{
            Comment comment = await _commentRepository.GetDefault(x => x.Id == id);

            if (comment != null)
            {
                var model = _mapper.Map<UpdateCommentDTO>(comment);

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

		public async Task<List<CommentVM>> GetComments()
        {
            var comments = await _commentRepository.GetFilteredList(
                select: x => new CommentVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    AppUserName = x.AppUser.UserName,
                    AppUserId = x.AppUser.Id,
                    PostTitle = x.Post.Title,
                    PostId = x.PostId
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Id),
                include: x => x.Include(x => x.AppUser).Include(x => x.Post)
                );

            return comments;
        }

		public async Task Update(UpdateCommentDTO model)
        {
            if (model != null)
            {
                var comment = _mapper.Map<Comment>(model);

                await _commentRepository.Update(comment);
            }
        }
	}
}
