namespace Presentation.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public bool Status { get; set; }
}
