using CleanArch26.Domain.Repository;
using CleanArch26.Domain.Users;
using CleanArch26.Infrastructure.Context;

namespace CleanArch26.Infrastructure.Repositories;
internal sealed class AppUserRepository : Repository<AppUser, ApplicationDbContext>, IAppUserRepository
{
    public AppUserRepository(ApplicationDbContext context) : base(context)
    {
    }
}