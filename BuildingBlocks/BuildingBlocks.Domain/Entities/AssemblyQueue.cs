namespace BuildingBlocks.Domain.Entities;

public class AssemblyQueue : TrackedEntity
{   
    public Guid OrderId { get; set; }
    public Enumerators.TaskStatus AssemblyStatus { get; set; }
    public bool IsDelivered { get; set; }
    public DateTimeOffset AssemblyDate { get; set; } = DateTimeOffset.Now;
}                     