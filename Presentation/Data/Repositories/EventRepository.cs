using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Data.Entities;
using Presentation.Interfaces;
using Presentation.Models;
using System.Linq.Expressions;

namespace Presentation.Data.Repositories;

public class EventRepository(EventDataContext context) : BaseRepository<EventEntity>(context), IEventRepository
{
    public override async Task<RepoResult<IEnumerable<EventEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.Include(i => i.Packages).ToListAsync();
            return new RepoResult<IEnumerable<EventEntity>> { Success = true, Data = entities };
        }
        catch (Exception ex)
        {
            return new RepoResult<IEnumerable<EventEntity>> { Success = false, Error = ex.Message };
        }
    }

    public override async Task<RepoResult<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.Include(i => i.Packages).FirstOrDefaultAsync(expression) ?? throw new Exception("Entity not found");
            return new RepoResult<EventEntity> { Success = true, Data = entity };
        }
        catch (Exception ex)
        {
            return new RepoResult<EventEntity> { Success = false, Error = ex.Message };
        }
    }
}