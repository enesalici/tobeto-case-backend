using Application.Repos;
using Core.Base.Repos.Concrete;
using Persistence.Contexts;

namespace Persistence.Repos;

public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
{
    public TaskRepository(DataBaseContext context) : base(context)
    {
    }
}
