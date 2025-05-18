using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Data.Repositories;
using Presentation.Interfaces;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("EventDBConnection")));
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService,EventService>();

var app = builder.Build();
app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventService API");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
