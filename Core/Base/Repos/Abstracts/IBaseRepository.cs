using Core.Base.Entities;

namespace Core.Base.Repos.Abstracts;

public interface IBaseRepository<TEntity> : IReadableRepository<TEntity>, IWritableRepository<TEntity> where TEntity : BaseEntity
{

}
