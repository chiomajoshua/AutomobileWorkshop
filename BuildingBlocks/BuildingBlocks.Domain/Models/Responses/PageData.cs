namespace BuildingBlocks.Domain.Models.Responses;

public record PageData<TEntity>
{
    public IEnumerable<TEntity>? EntityData { get; set; }
    public int TotalCount { get; set; }
}