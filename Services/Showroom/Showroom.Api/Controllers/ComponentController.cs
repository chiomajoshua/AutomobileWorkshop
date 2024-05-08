using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShowroomService.Core.Query.Components;
using ShowroomService.Data.Models.Requests.Components;
using Swashbuckle.AspNetCore.Annotations;

namespace ShowroomService.Api.Controllers;

/// <summary>
/// Component Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ComponentController : ControllerBase
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public ComponentController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets All Components
    /// </summary>
    /// <param name="paginationModel"></param>
    /// <param name="getComponentFilter"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [SwaggerResponse(200, type: typeof(AmwResponse))]
    [SwaggerResponse(404, type: typeof(AmwResponse))]
    [HttpGet, Route("getAllComponents")]
    public async Task<IActionResult> GetAllComponents([FromQuery] PaginationModel paginationModel, [FromQuery] GetComponentFilter getComponentFilter)
    {
        var response = await _mediator.Send(new GetComponentsQuery(paginationModel, getComponentFilter));
        return StatusCode(response.ResponseCode, response);
    }

    /// <summary>
    /// Gets Component By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [SwaggerResponse(200, type: typeof(AmwResponse))]
    [SwaggerResponse(404, type: typeof(AmwResponse))]
    [HttpGet, Route("getComponentById/{id}")]
    public async Task<IActionResult> GetComponentById(Guid id)
    {
        var response = await _mediator.Send(new GetComponentByIdQuery(id));
        return StatusCode(response.ResponseCode, response);
    }
}
