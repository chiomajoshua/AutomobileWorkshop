﻿namespace BuildingBlocks.Domain.Models.Requests;

public class PaginationModel
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SortBy { get; set; } = "";
}