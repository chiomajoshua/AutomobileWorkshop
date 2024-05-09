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
    public DbSet<AssemblyQueue> AssemblyQueue { get; set; }
    public DbSet<VehicleComponent> VehicleComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer", "wms");
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Customer");
            entity.Property(p => p.FirstName).HasColumnType("nvarchar(50)").HasComment("Customer's First Name");
            entity.Property(p => p.LastName).HasColumnType("nvarchar(50)").HasComment("Customer's Last Name");
            entity.Property(p => p.Email).HasColumnType("nvarchar(50)").HasComment("Customer's Email Address");
            entity.Property(p => p.Address).HasMaxLength(int.MaxValue).HasComment("The Customer's Address");
            entity.Property(p => p.CreatedDate).HasComment("The Date the Customer was Created");
            entity.Property(p => p.LastModifiedDate).HasComment("The Date the Customer was Last Modified");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Order");
            entity.Property(p => p.CustomerId).HasComment("The Identifier for the Customer");
            entity.Property(p => p.OrderStatus).HasComment("Current status of the order(Placed, Processing, Completed, Cancelled)");
            entity.Property(p => p.VehicleId).HasComment("The Identifier of the vehicle ordered");
            entity.Property(p => p.OrderDate).HasComment("The Date the Order was Placed");
            entity.Property(p => p.LastModifiedDate).HasComment("The Date the Order was Last Modified");
            entity.Property(p => p.TotalPrice).HasComment("TotalPrice on all items for this order");
            entity.Property(p => p.Quantity).HasComment("TotalPrice on all items for this order");
            entity.HasMany(d => d.OrderItems)
                  .WithOne(p => p.Order)
                  .HasForeignKey(d => d.OrderId);
        });            

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Order Item");
            entity.Property(p => p.OrderId).HasComment("The Identifier for the Order");
            entity.Property(p => p.PricePerUnit).HasComment("Price of the component at the time of order placement");
            entity.Property(p => p.ComponentId).HasComment("The Identifier of the component included in the order");
            entity.HasOne(d => d.Order)
                  .WithMany(p => p.OrderItems)
                  .HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Components", "Inventory");
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Component");
            entity.Property(p => p.ComponentType).HasComment("Type of component (Engine, Chassis, Option pack)");
            entity.Property(p => p.QuantityAvailable).HasComment("Number of available units of the component");
            entity.Property(p => p.LastModifiedDate).HasComment("The Last Date and Time the Component was Produced");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles", "Inventory");
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Vehicle");
            entity.Property(p => p.Model).HasComment("Model of the vehicle");
            entity.Property(p => p.QuantityAvailable).HasComment("Number of available units of the vehicle");
            entity.Property(p => p.Price).HasComment("Price of the vehicle");
            entity.Property(p => p.Manufacturer).HasComment("Manufacturer of the vehicle");
            entity.Property(p => p.Year).HasComment("Year the vehicle was produced");
        });

        modelBuilder.Entity<VehicleComponent>(entity =>
        {
            entity.ToTable("VehicleComponent", "Inventory");
            entity.HasKey(vc => new { vc.VehicleId, vc.ComponentId });
           entity.HasOne(vc => vc.Vehicle)
            .WithMany(v => v.VehicleComponents)
            .HasForeignKey(vc => vc.VehicleId);

            entity.HasOne(vc => vc.Component)
            .WithMany(c => c.VehicleComponents)
            .HasForeignKey(vc => vc.ComponentId);
        });

        modelBuilder.Entity<ProductionQueue>(entity =>
        {
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Production task");
            entity.Property(p => p.ComponentId).HasComment("The Identifier of the component to be produced");
            entity.Property(p => p.Quantity).HasComment("Number of units to be produced");
            entity.Property(p => p.ProductionStatus).HasComment("Status of the production task (Pending, InProgress, Completed");
            entity.Property(p => p.ProductionDate).HasComment("The Date and Time this Production Task was Initiated");
        });

        modelBuilder.Entity<AssemblyQueue>(entity =>
        {
            entity.Property(p => p.Id).HasComment("The Unique identifier for the Assembly task");           
            entity.Property(p => p.AssemblyStatus).HasComment("Status of the assembly task (Pending, InProgress, Completed");
            entity.Property(p => p.AssemblyDate).HasComment("The Date and Time this Assembly Task was Initiated");           
        });
    }
}
