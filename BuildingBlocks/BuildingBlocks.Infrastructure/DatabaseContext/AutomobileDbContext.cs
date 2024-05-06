using BuildingBlocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.DatabaseContext;

public class AutomobileDbContext : DbContext
{
    public AutomobileDbContext(DbContextOptions<AutomobileDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Component> Components {  get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ProductionQueue> ProductionQueue {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(p => p.FirstName).HasColumnType("nvarchar(50)").HasComment("My Column Description");
            entity.Property(p => p.LastName).HasColumnType("nvarchar(50)").HasComment("My Column Description");
            entity.Property(p => p.Email).HasColumnType("nvarchar(50)").HasComment("My Column Description");
            entity.Property(p => p.Address).HasMaxLength(int.MaxValue).HasComment("My Column Description");
            entity.HasMany(d => d.Orders)
                  .WithOne(p => p.Customer)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Order>(entity =>
        {                  
            entity.HasMany(d => d.OrderItems)
                  .WithOne(p => p.Order)
                  .HasForeignKey(d => d.OrderId);
        });
    }
}
