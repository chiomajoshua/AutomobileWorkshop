namespace BuildingBlocks.Domain.Models.Responses;

public abstract record PageDetails
{
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public int PageSize { get; set; } = 0;
}