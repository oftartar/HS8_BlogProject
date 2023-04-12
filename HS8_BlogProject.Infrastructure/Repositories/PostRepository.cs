using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Repositories;

namespace HS8_BlogProject.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }
    }
}
