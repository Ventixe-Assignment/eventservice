using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public bool Status { get; set; }

    public ICollection<EventPackageEntity> Packages { get; set; } = [];
}   

