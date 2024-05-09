using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShowroomService.Core.Query.Components;
using ShowroomService.Core.Query.Vehicles;
using ShowroomService.Data.Models.Requests.Components;
using ShowroomService.Data.Models.Requests.Vehicles;
using Swashbuckle.AspNetCore.Annotations;

namespace ShowroomService.Api.Controllers;

/// <summary>
/// Warehouse Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public WarehouseController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets All Vehicles
    /// </summary>
    /// <param name="paginationModel"></param>
    /// <param name="getVehicleFilter"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [SwaggerResponse(200, type: typeof(AmwResponse))]
    [SwaggerResponse(404, type: typeof(AmwResponse))]
    [HttpGet, Route("getAllVehicles")]
    public async Task<IActionResult> GetAllVehicles([FromQuery] PaginationModel paginationModel, [FromQuery] GetVehicleFilter getVehicleFilter)
    {
        var response = await _mediator.Send(new GetVehiclesQuery(paginationModel, getVehicleFilter));
        return StatusCode(response.ResponseCode, response);
    }

    /// <summary>
    /// Gets Vehicle By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [SwaggerResponse(200, type: typeof(AmwResponse))]
    [SwaggerResponse(404, type: typeof(AmwResponse))]
    [HttpGet, Route("getVehicleById/{id}")]
    public async Task<IActionResult> GetVehicleById(Guid id)
    {
        var response = await _mediator.Send(new GetVehicleByIdQuery(id));
        return StatusCode(response.ResponseCode, response);
    }

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
