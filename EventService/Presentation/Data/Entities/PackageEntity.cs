using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Data.Entities;

public class PackageEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? SeatTier { get; set; }
    public string? SeatPlacement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public  decimal? Price { get; set; }
    public string? Currency { get; set; }

}

