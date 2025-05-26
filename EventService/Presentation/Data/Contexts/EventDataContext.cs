using Microsoft.EntityFrameworkCore;
using Presentation.Data.Entities;

namespace Presentation.Data.Contexts;

public class EventDataContext(DbContextOptions<EventDataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<PackageEntity> Packages { get; set; }
    public DbSet<EventPackageEntity> EventPackages { get; set; }
}
