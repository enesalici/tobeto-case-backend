using Core.Base.Repos.Concrete;
using Domain.Entities;
using Persistence.Contexts;
using Application.Repos;

namespace Persistence.Repos;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataBaseContext context) : base(context)
    {
    }
}
