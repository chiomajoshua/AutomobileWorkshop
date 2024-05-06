using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class ProductionQueue : TrackedEntity
{
    public Guid ComponentId { get; set; }    
    public int Quantity { get; set; }
    public ProductionStatus ProductionStatus { get; set; }
    public DateTimeOffset ProductionDate { get; set; }
    public virtual Component Component { get; set; } = null!;
}