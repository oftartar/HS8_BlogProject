using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Repositories;

namespace HS8_BlogProject.Infrastructure.Repositories
{
	public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public LikeRepository(AppDbContext context) : base(context) { }
    }
}
