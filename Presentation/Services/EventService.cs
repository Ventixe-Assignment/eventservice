using Presentation.Data.Entities;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Services;

 
public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;


    public async Task<EventResult> CreateEventAsync(EventRequest request)
    {
        try
        {
            var entity = new EventEntity
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Location = request.Location,
                StartDate = request.StartDate,
                Status = request.Status
            };

            var result = await _eventRepository.AddAsync(entity);
            return result.Success
                ? new EventResult { Success = true }
                : new EventResult { Success = false, Error = result.Error };
        }
        catch (Exception ex)
        {
            return new EventResult { Success = false, Error = ex.Message };
        }
    }
    public async Task<EventResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();
        var mappedModel = result.Data?.Select(e => new Event
        {
            Id = e.Id,
            Name = e.Name,
            Category = e.Category,
            Description = e.Description,
            ImageUrl = e.ImageUrl,
            Location = e.Location,
            StartDate = e.StartDate,
            Status = e.Status
        });

        return new EventResult<IEnumerable<Event>> { Success = true, Data = mappedModel };
    }

    public async Task<EventResult<Event?>> GetEventAsync(string id)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == id);
        if (result.Success && result.Data != null)
        {
            var mappedModel = new Event
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Category = result.Data.Category,
                Description = result.Data.Description,
                ImageUrl = result.Data.ImageUrl,
                Location = result.Data.Location,
                StartDate = result.Data.StartDate,
                Status = result.Data.Status
            };

            return new EventResult<Event?> { Success = true, Data = mappedModel };
        }

        return new EventResult<Event?> { Success = false, Error = "Event Not Found" };
    }
}
