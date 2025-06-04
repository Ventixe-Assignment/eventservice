using Presentation.Data.Contexts;
using Presentation.Data.Entities;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<EventDataContext>()
    .UseSqlServer("Server=tcp:jannes-ventixe-sqlserver.database.windows.net,1433;Initial Catalog=database;Persist Security Info=False;User ID=janne.heikkinen;Password=Bytmig1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;") // Use the same connection string as your main app
    .Options;

using var context = new EventDataContext(options);

// Get all events and all packages
var allEvents = context.Events.ToList();
var allPackages = context.Packages.ToList();

foreach (var ev in allEvents)
{
    foreach (var pkg in allPackages)
    {
        // Avoid duplicates
        if (!context.EventPackages.Any(ep => ep.EventId == ev.Id && ep.PackageId == pkg.Id))
        {
            context.EventPackages.Add(new EventPackageEntity
            {
                EventId = ev.Id,
                PackageId = pkg.Id
            });
        }
    }
}

context.SaveChanges();

Console.WriteLine("All packages have been added to all events.");