using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.PostDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Application.Models.VMs.CommentVMs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Application.Models.VMs.LikeVMs;
using HS8_BlogProject.Application.Models.VMs.PostVMs;
using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Enums;
using HS8_BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace HS8_BlogProject.Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _mapper = mapper;
        }
        public async Task Create(CreatePostDTO model)
        {
            if (model != null)
            {
                Post post = _mapper.Map<Post>(model);

                if (post.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    // Resize
                    image.Mutate(x => x.Resize(600, 560));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");

                    post.ImagePath = $"/images/{guid}.jpg";
                }

                await _postRepository.Create(post);
            }
        }

        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
                Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)),

                Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ImagePath = x.ImagePath,
                        AppUserId = x.AppUserId,
                        AppUserName = x.AppUser.UserName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName),
                    include: x => x.Include(x => x.AppUser))
            };

            return model;
        }

        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);

            if (post != null)
            {
                post.DeleteDate = DateTime.Now;
                post.Status = Status.Passive;

                await _postRepository.Delete(post);
            }
        }

        public async Task<UpdatePostDTO> GetById(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);

            if (post != null)
            {
                var model = _mapper.Map<UpdatePostDTO>(post);

                model.Genres = await _genreRepository.GetFilteredList(
                        select: x => new GenreVM
                        {
                            Id = x.Id,
                            Name = x.Name,
                        },
                        where: x => x.Status != Status.Passive,
                        orderBy: x => x.OrderBy(x => x.Name));

                model.Authors = await _authorRepository.GetFilteredList(
                        select: x => new AuthorVM
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            ImagePath = x.ImagePath,
                            AppUserId = x.AppUserId,
                            AppUserName = x.AppUser.UserName
                        },
                        where: x => x.Status != Status.Passive,
                        orderBy: x => x.OrderBy(x => x.FirstName),
                        include: x => x.Include(x => x.AppUser));

                return model;
            }
            return null;
        }

        public async Task<List<PostVM>> GetPosts()
        {
            var posts = await _postRepository.GetFilteredList(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    GenreId = x.Genre.Id,
                    GenreName = x.Genre.Name,
                    AuthorId = x.Author.Id,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Genre).Include(x => x.Author)
                );

            return posts;
        }

        public async Task Update(UpdatePostDTO model)
        {
            if (model != null)
            {
                var post = _mapper.Map<Post>(model);

                if (post.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    image.Mutate(x => x.Resize(600, 560));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");

                    post.ImagePath = $"/images/{guid}.jpg";
                }

                await _postRepository.Update(post);
            }
        }

        public async Task<PostDetailsVM> GetPostDetails(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailsVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    GenreName = x.Genre.Name
                },
                where: x => x.Id == id,
                include: x => x.Include(x => x.Author).Include(x => x.Genre));

            if (post != null)
            {
                post.Comments = await _commentRepository.GetFilteredList(
                    select: x => new CommentVM
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Content = x.Content,
                        AppUserName = x.AppUser.UserName,
                        PostTitle = x.Post.Title,
                        AppUserId = x.AppUserId,
                        PostId = x.PostId
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Title),
                    include: x => x.Include(x => x.AppUser).Include(x => x.Post)
                    );

                post.Likes = await _likeRepository.GetFilteredList(
                    select: x => new LikeVM
                    {
                        Id = x.Id,
                        AppUserName = x.AppUser.UserName,
                        PostTitle = x.Post.Title,
                        AppUserId = x.AppUserId,
                        PostId = x.PostId
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Id),
                    include: x => x.Include(x => x.AppUser).Include(x => x.Post)
                    );
            }

            return post;
        }

        public async Task<List<PostDetailsVM>> GetPostsForMember()
        {
            var posts = await _postRepository.GetFilteredList(
                select: x => new PostDetailsVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    GenreName = x.Genre.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate)
                );

            foreach (var post in posts)
            {
                post.Comments = await _commentRepository.GetFilteredList(
                select: x => new CommentVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    AppUserName = x.AppUser.UserName,
                    PostTitle = x.Post.Title,
                    AppUserId = x.AppUserId,
                    PostId = x.PostId
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.AppUser).Include(x => x.Post)
                );

                post.Likes = await _likeRepository.GetFilteredList(
                    select: x => new LikeVM
                    {
                        Id = x.Id,
                        AppUserName = x.AppUser.UserName,
                        PostTitle = x.Post.Title,
                        AppUserId = x.AppUserId,
                        PostId = x.PostId
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Id),
                    include: x => x.Include(x => x.AppUser).Include(x => x.Post)
                    );
            }

            return posts;
        }
    }
}
