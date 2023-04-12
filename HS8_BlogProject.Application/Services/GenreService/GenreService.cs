using AutoMapper;
using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Enums;
using HS8_BlogProject.Domain.Repositories;
using HS8_BlogProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Services.GenreService
{
    internal class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateGenreDTO model)
        {
            if (model != null)
            {
                Genre genre = _mapper.Map<Genre>(model);

                await _genreRepository.Create(genre);
            }
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);

            if (genre != null)
            {
                genre.DeleteDate = DateTime.Now;
                genre.Status = Status.Passive;

                await _genreRepository.Delete(genre);
            }
        }

        public async Task<UpdateGenreDTO> GetById(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);

            if (genre != null)
            {
                var model = _mapper.Map<UpdateGenreDTO>(genre);

                return model;
            }

            return null;
        }

        public async Task<List<GenreVM>> GetGenres()
        {
            var genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM()
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)
                );

            return genres;
        }

        public async Task Update(UpdateGenreDTO model)
        {
            if (model != null)
            {
                var genre = _mapper.Map<Genre>(model);

                await _genreRepository.Update(genre);
            }
        }
    }
}
