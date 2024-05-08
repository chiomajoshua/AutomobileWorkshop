using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Domain.Entities;

public abstract class TrackedEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();    
}