﻿namespace DotnetAngularMiniEcommerce_API.Domain.Entities
{
    public class ProductImageFile : File
    {
        public ICollection<Product> Products { get; set; }
    }
}
