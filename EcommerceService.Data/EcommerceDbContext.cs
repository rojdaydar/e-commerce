using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore;
namespace EcommerceService.Data;

public class EcommerceDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
        
    public EcommerceDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>();
        modelBuilder.Entity<Product>();
        modelBuilder.Entity<Campaign>();
        base.OnModelCreating(modelBuilder);
    }
}