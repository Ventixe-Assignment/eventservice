using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Interfaces;
using Presentation.Models;
using System.Linq.Expressions;

namespace Presentation.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<RepoResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();

            return new RepoResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepoResult { Success = false, Error = ex.Message };
        }
    }

    public virtual async Task<RepoResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.ToListAsync();
            return new RepoResult<IEnumerable<TEntity>> { Success = true, Data = entities };
        }
        catch (Exception ex)
        {
            return new RepoResult<IEnumerable<TEntity>> { Success = false, Error = ex.Message };
        }
    }

    public virtual async Task<RepoResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(expression) ?? throw new Exception("Entity not found");
            return new RepoResult<TEntity> { Success = true, Data = entity };
        }
        catch (Exception ex)
        {
            return new RepoResult<TEntity> { Success = false, Error = ex.Message };
        }
    }

    public virtual async Task<RepoResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();

            return new RepoResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepoResult { Success = false, Error = ex.Message };
        }
    }

    public virtual async Task<RepoResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();

            return new RepoResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepoResult { Success = false, Error = ex.Message };
        }
    }

    public virtual async Task<RepoResult> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {

        var result = await _table.AnyAsync(expression);
        return (result)
            ? new RepoResult { Success = true }
            : new RepoResult { Success = false, Error = "Not found" };

    }
}
