namespace Washateria.Database;

using Microsoft.EntityFrameworkCore;
using Washateria.Models;

class WashateriaDb : DbContext
{
    public WashateriaDb(DbContextOptions<WashateriaDb> options) : base(options) {}
    public DbSet<Washer> Washers => Set<Washer>();
}