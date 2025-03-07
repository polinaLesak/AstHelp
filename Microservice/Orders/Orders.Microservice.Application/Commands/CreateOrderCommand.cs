﻿using MediatR;
using Orders.Microservice.Application.DTOs;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public int CustomerId { get; set; }
        public string ReasonForIssue { get; set; } = "";
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
