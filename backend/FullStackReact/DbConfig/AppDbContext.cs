using FullStackReact.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStackReact.DbConfig;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    { }
    
    public DbSet<Planet> Planets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Planet>().ToTable("Planets");
    }
}