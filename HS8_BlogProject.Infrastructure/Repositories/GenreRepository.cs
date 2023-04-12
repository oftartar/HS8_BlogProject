using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Repositories;

namespace HS8_BlogProject.Infrastructure.Repositories
{
	public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context) { }
    }
}
