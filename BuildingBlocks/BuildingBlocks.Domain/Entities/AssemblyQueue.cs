namespace BuildingBlocks.Domain.Entities;

public class AssemblyQueue : TrackedEntity
{
    public Guid OrderId { get; set; }
    public Guid ComponentId { get; set; }
    public int Quantity { get; set; }
    public Enumerators.TaskStatus AssemblyStatus { get; set; }
    public DateTimeOffset AssemblyDate { get; set; }
    public virtual Component Component { get; set; } = null!;
}