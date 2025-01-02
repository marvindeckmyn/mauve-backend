using Microsoft.EntityFrameworkCore;
using Mauve.Core.Models;

namespace Mauve.Infrastructure.Data;

public class MauveDbContext : DbContext
{
    public MauveDbContext(DbContextOptions<MauveDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<SupplierInfo> SupplierInfos { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product Configuration
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.SupplierInfo)
            .WithOne(s => s.Product)
            .HasForeignKey<SupplierInfo>(s => s.ProductId);

        // Convert string list to JSON
        modelBuilder.Entity<Product>()
            .Property(p => p.AdditionalImages)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        
        modelBuilder.Entity<Product>()
            .Property(p => p.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        // Order configuration
        modelBuilder.Entity<Order>()
            .Property(o => o.SubTotal)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.ShippingCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId);

        // User configuration
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Addresses)
            .WithOne();

        modelBuilder.Entity<User>()
            .HasMany(u => u.WishList)
            .WithMany();
    }
}