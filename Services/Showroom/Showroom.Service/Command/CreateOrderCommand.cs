using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using Showroom.Data.Models.Requests;

namespace Showroom.Service.Command;

public record CreateOrderCommand(CreateOrderRequest CreateOrderRequest) : IRequest<AmwResponse>;