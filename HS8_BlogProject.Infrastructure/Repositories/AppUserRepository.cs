using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Repositories;

namespace HS8_BlogProject.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context) : base(context) { }
    }
}
