namespace BuildingBlocks.Domain.Entities;

public class AssemblyQueue : TrackedEntity
{       
    public Enumerators.TaskStatus AssemblyStatus { get; set; }
    public DateTimeOffset AssemblyDate { get; set; }
}                     