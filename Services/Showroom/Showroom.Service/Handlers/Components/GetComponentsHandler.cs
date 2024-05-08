using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using ShowroomService.Core.Query.Components;
using ShowroomService.Core.Services.Components.Contracts;
using ShowroomService.Data.Models.MappingExtensions;
using ShowroomService.Data.Models.Responses.Components;

namespace ShowroomService.Core.Handlers.Components;

public class GetComponentsHandler : IRequestHandler<GetComponentsQuery, AmwResponse>
{
    private readonly IComponentService _componentService;
    public GetComponentsHandler(IComponentService componentService)
    {
        _componentService = componentService;
    }

    public async Task<AmwResponse> Handle(GetComponentsQuery request, CancellationToken cancellationToken)
    {
        var components = await _componentService.GetComponentsAsync(request.PaginationModel, request.GetComponentFilter);
        return AmwResponse.ExistsResponse(new ComponentListResponse
        {
            Components = components.EntityData?.ComponentResponseList(),
            CurrentPage = request.PaginationModel.Page,
            PageSize = request.PaginationModel.PageSize,
            TotalPages = (int)Math.Ceiling(components.TotalCount / (double)request.PaginationModel.PageSize)
        });
    }
}