using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Data.Entities;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService, EventDataContext context) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
    private readonly EventDataContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(EventRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _eventService.CreateEventAsync(request);
        return result.Success ? Ok() : StatusCode(500, result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _eventService.GetEventsAsync();
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _eventService.GetEventAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost("{eventId}/packages")]
    public async Task<IActionResult> AddPackagesToEvent(string eventId, [FromBody] List<int> packageIds)
    {
        var eventExists = await _context.Events.AnyAsync(e => e.Id == eventId);
        if (!eventExists) return NotFound("Event not found.");

        foreach (var packageId in packageIds)
        {
            var alreadyLinked = await _context.EventPackages
                .AnyAsync(ep => ep.EventId == eventId && ep.PackageId == packageId);

            if (!alreadyLinked)
            {
                _context.EventPackages.Add(new EventPackageEntity
                {
                    EventId = eventId,
                    PackageId = packageId
                });
            }
        }

        await _context.SaveChangesAsync();
        return Ok("Packages linked to event.");
    }
}