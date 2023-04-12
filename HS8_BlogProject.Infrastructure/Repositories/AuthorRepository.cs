using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Repositories;

namespace HS8_BlogProject.Infrastructure.Repositories
{
	public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context) { }
    }
}
