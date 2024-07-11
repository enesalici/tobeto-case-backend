using Core.Base.Entities;
using Core.Base.Repos.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Base.Repos.Concrete;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> data = _context.Set<TEntity>();

        if (include != null)
            data = include(data);

        return await data.FirstOrDefaultAsync(predicate);
    }
    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> data = _context.Set<TEntity>();

        if (predicate != null)
            data = data.Where(predicate);
        if (include != null)
            data = include(data);

        return await data.ToListAsync();
    }
    public async Task AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
