﻿using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Application.Requestparameters;
using DotnetAngularMiniEcommerce_API.Application.Services;
using DotnetAngularMiniEcommerce_API.Application.Validators.Products;
using DotnetAngularMiniEcommerce_API.Application.ViewModels.Products;
using DotnetAngularMiniEcommerce_API.Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace DotnetAngularMiniEcommerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CreateProductValidator _createProductValidator;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public ProductsController(
            CreateProductValidator createProductValidator,
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            _createProductValidator = createProductValidator;
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination) 
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false)
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size)
                .Select(p => new
            {
                p.ID,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            });

            return Ok(new {
                totalCount,
                products 
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            };

            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.ID);
            
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;

            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload() {
            var files = Request.Form.Files;
            await _fileService.UploadAsync("resources/product-images", files);
            return Ok();
        }
    }
}
