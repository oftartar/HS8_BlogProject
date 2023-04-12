using HS8_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS8_BlogProject.Application.Models.VMs.AuthorVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Services.AuthorService
{
    public interface IAuthorService
	{
		Task Create(CreateAuthorDTO model);
		Task<CreateAuthorDTO> CreateAuthor();
		Task Update(UpdateAuthorDTO model);
		Task Delete(int id);
		Task<UpdateAuthorDTO> GetById(int id);
		Task<List<AuthorVM>> GetAuthors();
	}
}
