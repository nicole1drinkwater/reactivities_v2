using Application.Activities.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Migrations;
using Application.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000","https://localhost:3000"));

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
