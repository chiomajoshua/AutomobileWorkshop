﻿using BuildingBlocks.Domain.Models.Responses;

namespace ShowroomService.Data.Models.Responses.Components;

public record ComponentListResponse : PageDetails
{
    public List<ComponentResponse> Components { get; set; }
}