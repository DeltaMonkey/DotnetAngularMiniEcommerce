﻿using DotnetAngularMiniEcommerce_API.Domain.Entities.Common;

namespace DotnetAngularMiniEcommerce_API.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock  { get; set; }
        public long Price { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
