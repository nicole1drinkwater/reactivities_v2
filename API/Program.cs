using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>( opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

try
{
    var context = service.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await new DbInitializer().SeedData(context);
}
catch (System.Exception)
{
    var logger = service.GetRequiredService<ILogger<Program>>();
    logger.LogError("An error occurred during migration.");
}
app.Run();
