using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Events : TrackedEntity
{
    public string EventPayload { get; set; } = null!;
    public EventType EventType { get; set; }
    public DateTimeOffset EventDate { get; set; } = DateTimeOffset.Now;
}