using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.VMs.AppUserVMs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Enums;
using HS8_BlogProject.Domain.Repositories;

namespace HS8_BlogProject.Application.Services.AuthorService
{
    internal class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IAppUserRepository appUserRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            if (model != null)
            {
                Author author = _mapper.Map<Author>(model);

                await _authorRepository.Create(author);
            }
        }

        public async Task<CreateAuthorDTO> CreateAuthor()
        {
            CreateAuthorDTO model = new CreateAuthorDTO()
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
                    orderBy: x => x.OrderBy(x => x.UserName))
            };

            return model;
        }

        public async Task Delete(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);

            if (author != null)
            {
                author.DeleteDate = DateTime.Now;
                author.Status = Status.Passive;

                await _authorRepository.Delete(author);
            }
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            var authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName)
                );

            return authors;
        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);

            if (author != null)
            {
                var model = _mapper.Map<UpdateAuthorDTO>(author);

                //Todo: Gerekirse AppUser Listesi ekle.
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

                return model;
            }

            return null;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            if (model != null)
            {
                var author = _mapper.Map<Author>(model);

                await _authorRepository.Update(author);
            }
        }
    }
}
