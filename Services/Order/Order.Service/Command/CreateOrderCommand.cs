﻿using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using OrderService.Data.Models.Requests;

namespace OrderService.Core.Command;

public record CreateOrderCommand(CreateOrderRequest CreateOrderRequest) : IRequest<AmwResponse>;