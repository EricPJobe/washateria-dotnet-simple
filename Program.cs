using Microsoft.EntityFrameworkCore;
using Washateria.Database;
using Washateria.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WashateriaDb>(opt => opt.UseInMemoryDatabase("Washers"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/washers", async (WashateriaDb db) => 
    await db.Washers.ToListAsync());

app.MapGet("/washers/active", async (WashateriaDb db) => 
    await db.Washers.Where(t => t.IsActive).ToListAsync());

app.MapGet("/washers/{id}", async (int id, WashateriaDb db) => 
    await db.Washers.FindAsync(id)
        is Washer washer
            ? Results.Ok(washer)
            : Results.NotFound());

app.MapPost("/washers", async (Washer washer, WashateriaDb db) =>
{
    db.Washers.Add(washer);
    await db.SaveChangesAsync();

    return Results.Created($"/washers/{washer.Id}", washer);
});

app.MapPut("/washers/{id}", async (int id, Washer inputWasher, WashateriaDb db) => 
{
    var washer = await db.Washers.FindAsync(id);
    if (washer is null) return Results.NotFound();

    washer.Designation = inputWasher.Designation;
    washer.Manufacturer = inputWasher.Manufacturer;
    washer.ModelNumber = inputWasher.ModelNumber;
    washer.LastMaintenanceTs = inputWasher.LastMaintenanceTs;
    washer.UpdatedTs = DateTime.Now;
    washer.IsActive = inputWasher.IsActive;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
 

app.Run();
