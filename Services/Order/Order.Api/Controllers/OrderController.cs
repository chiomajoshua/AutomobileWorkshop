using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Command;
using OrderService.Data.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Api.Controllers;

/// <summary>
/// Order Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public OrderController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Creates an Order
    /// </summary>
    /// <param name="createOrderRequest"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [SwaggerResponse(201, type: typeof(AmwResponse))]
    [SwaggerResponse(404, type: typeof(AmwResponse))]
    [HttpPost, Route("CreateOrder")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
    {
        var response = await _mediator.Send(new CreateOrderCommand(createOrderRequest));
        return StatusCode(response.ResponseCode, response);
    }

    /// <summary>
    /// Cancels an Order
    /// </summary>
    /// <param name="cancelOrderRequest"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [SwaggerResponse(200, type: typeof(AmwResponse))]
    [SwaggerResponse(404, type: typeof(AmwResponse))]
    [SwaggerResponse(500, type: typeof(AmwResponse))]
    [HttpPatch, Route("CancelOrder")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest cancelOrderRequest)
    {
        var response = await _mediator.Send(new CancelOrderCommand(cancelOrderRequest));
        return StatusCode(response.ResponseCode, response);
    }
}