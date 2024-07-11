using Core.Base.Entities;

namespace Core.Base.Repos.Abstracts;

public interface IWritableRepository<TEntity> where TEntity : BaseEntity
{
	Task AddAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
	Task DeleteAsync(TEntity entity);
}
