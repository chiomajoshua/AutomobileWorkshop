using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using ShowroomService.Core.Query.Components;
using ShowroomService.Core.Services.Components.Contracts;

namespace ShowroomService.Core.Handlers.Components;

public class GetComponentByIdHandler : IRequestHandler<GetComponentByIdQuery, AmwResponse>
{
    private readonly IComponentService _componentService;
    public GetComponentByIdHandler(IComponentService componentService)
    {
        _componentService = componentService;
    }

    public async Task<AmwResponse> Handle(GetComponentByIdQuery request, CancellationToken cancellationToken)
    {
        var component = await _componentService.GetComponentByIdAsync(request.ComponentId);
        return AmwResponse.ExistsResponse(component);
    }
}