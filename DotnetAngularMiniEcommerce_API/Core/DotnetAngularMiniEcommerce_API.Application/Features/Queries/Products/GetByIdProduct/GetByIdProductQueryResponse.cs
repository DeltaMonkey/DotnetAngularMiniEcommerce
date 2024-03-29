﻿using DotnetAngularMiniEcommerce_API.Domain.Entities;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.Products.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
