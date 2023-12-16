using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class StoreDbContext : DbContext
{
    public StoreDbContext()
    {
        
    }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductInStore> ProductsInStore { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=market;");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Store>()
            .HasMany(s => s.Products)
            .WithMany(p => p.Stores)
            .UsingEntity<ProductInStore>(
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(t => t.ProductsInStore)
                    .HasForeignKey(pt => pt.ProductName),
                j => j
                    .HasOne(pt => pt.Store)
                    .WithMany(p => p.ProductsInStore)
                    .HasForeignKey(pt => pt.StoreId),
                j =>
                {
                    j.Property(pt => pt.Quantity).HasDefaultValue(0);
                    j.Property(pt => pt.Price).HasDefaultValue(0);
                    j.HasKey(t => new {t.StoreId, t.ProductName});
                    j.ToTable("ProductsInStore");
                });
    }
    
}