using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models;

public class Package
{
    public int Id { get; set; }
    public string EventId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? SeatTier { get; set; }
    public string? SeatPlacement { get; set; }
    public decimal? Price { get; set; }
    public string? Currency { get; set; }

}
