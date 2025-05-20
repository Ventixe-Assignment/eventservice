using Presentation.Models;

namespace Presentation.Interfaces
{
    public interface IEventService
    {
        Task<EventResult> CreateEventAsync(EventRequest request);
        Task<EventResult<Event?>> GetEventAsync(string id);
        Task<EventResult<IEnumerable<Event>>> GetEventsAsync();
    }
}