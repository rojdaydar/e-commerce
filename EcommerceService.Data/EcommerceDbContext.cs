using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceService.Data;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Campaign> Campaigns { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>();
        modelBuilder.Entity<Product>();
        modelBuilder.Entity<Campaign>();
        base.OnModelCreating(modelBuilder);
    }
}