using Microsoft.EntityFrameworkCore;
using OnionAPI.Domain.Entities;
using System.Reflection;

namespace OnionAPI.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    { }

    public AppDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Detail> Details { get; set; }

    // coka cok iliskiyi ef kendisi oluşturacak

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Configurations dosyalarını tek tek eklemek yerine assembly ile aldık
    }
}
