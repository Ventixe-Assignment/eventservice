using Presentation.Data;

namespace Presentation.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventEntity>> GetAllAsync();
        Task<EventEntity?> GetAsync(string id);
    }
}