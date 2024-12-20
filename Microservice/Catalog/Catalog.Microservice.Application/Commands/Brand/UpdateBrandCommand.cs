﻿using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class UpdateBrandCommand : IRequest<Brand>
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = "";
    }
}
