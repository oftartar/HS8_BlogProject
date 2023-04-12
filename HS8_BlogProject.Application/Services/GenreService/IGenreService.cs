using HS8_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS8_BlogProject.Application.Models.VMs.GenreVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Services.GenreService
{
    public interface IGenreService
	{
		Task Create(CreateGenreDTO model);
		Task Update(UpdateGenreDTO model);
		Task Delete(int id);
		Task<UpdateGenreDTO> GetById(int id);
		Task<List<GenreVM>> GetGenres();
	}
}
