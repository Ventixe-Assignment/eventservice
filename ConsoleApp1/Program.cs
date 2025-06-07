using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Data.Entities;

var optionsBuilder = new DbContextOptionsBuilder<EventDataContext>();
optionsBuilder.UseSqlServer("Server=tcp:jannes-ventixe-sqlserver.database.windows.net,1433;Initial Catalog=database;Persist Security Info=False;User ID=janne.heikkinen;Password=Bytmig1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"); // <-- Replace with your actual connection string

using var context = new EventDataContext(optionsBuilder.Options);

// Load all events including their linked packages
var allEvents = await context.Events
    .Include(e => e.Packages)
    .ToListAsync();

if (!allEvents.Any())
{
    Console.WriteLine("No events found.");
    return;
}

// Define your packages to add
var packages = new List<PackageEntity>
{
    new PackageEntity { Id = 1, Name = "Diamond Spark", Price = 100m, Currency = "USD" },
    new PackageEntity { Id = 2, Name = "Sapphire Shine", Price = 250m, Currency = "USD" },
    new PackageEntity { Id = 3, Name = "Emerald Luxe", Price = 400m, Currency = "USD" },
    new PackageEntity { Id = 4, Name = "Ruby Radiance", Price = 600m, Currency = "USD" },
    new PackageEntity { Id = 5, Name = "VIP Diamond Elite", Price = 1000m, Currency = "USD" }
};

// Make sure packages exist in the DB
foreach (var package in packages)
{
    var existingPackage = await context.Packages.FindAsync(package.Id);
    if (existingPackage == null)
    {
        context.Packages.Add(package);
        await context.SaveChangesAsync(); // save after adding package
    }
}

// Reload packages from DB after adding
var allPackagesInDb = await context.Packages.ToListAsync();

// Link all packages to every event
foreach (var eventEntity in allEvents)
{
    foreach (var package in allPackagesInDb)
    {
        bool alreadyLinked = eventEntity.Packages.Any(ep => ep.PackageId == package.Id);
        if (!alreadyLinked)
        {
            eventEntity.Packages.Add(new EventPackageEntity
            {
                EventId = eventEntity.Id,
                PackageId = package.Id
            });
        }
    }
}

await context.SaveChangesAsync();

Console.WriteLine("All packages have been added to all events successfully.");