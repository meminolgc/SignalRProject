﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService ProductService, IMapper mapper)
        {
            _productService = ProductService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            return Ok(_productService.TProductCount());
        }

		[HttpGet("ProductNameByMaxPrice")]
		public IActionResult ProductNameByMaxPrice()
		{
			return Ok(_productService.TProductNameByMaxPrice());
		}

		[HttpGet("ProductNameByMinPrice")]
		public IActionResult ProductNameByMinPrice()
		{
			return Ok(_productService.TProductNameByMinPrice());
		}

		[HttpGet("ProductAvgPriceByHamburger")]
		public IActionResult ProductAvgPriceByHamburger()
		{
			return Ok(_productService.TProductAvgPriceByHamburger());
		}

		[HttpGet("ProductCountByHamburger")]
		public IActionResult ProductCountByHamburger()
		{
			return Ok(_productService.TProductCountByCategoryNameHamburger());
		}

		[HttpGet("ProductCountByDrink")]
		public IActionResult ProductCountByDrink()
		{
			return Ok(_productService.TProductCountByCategoryNameDrink());
		}

		[HttpGet("ProductPriceAvg")]
		public IActionResult ProductPriceAvg()
		{
			return Ok(_productService.TProductPriceAvg());
		}

		[HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(x => x.Category).Select(y => new ResultProductWithCategory
            {
                Description = y.Description,
                ImageUrl = y.ImageUrl,
                Price = y.Price,
                ProductID = y.ProductID,
                ProductName = y.ProductName,
                ProductStatus = y.ProductStatus,
                CategoryName = y.Category.CategoryName
            }).ToList();
            return Ok(values.ToList());
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            _productService.TAdd(new Product()
            {
                Description = createProductDto.Description,
                ImageUrl = createProductDto.ImageUrl,
                Price = createProductDto.Price,
                ProductName = createProductDto.ProductName,
                ProductStatus = createProductDto.ProductStatus,
                CategoryID = createProductDto.CategoryID
            });
            return Ok("Ürün Eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetByID(id);
            _productService.TDelete(value);
            return Ok("Ürün Silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _productService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            _productService.TUpdate(new Product()
            {
                ProductStatus = updateProductDto.ProductStatus,
                ProductName = updateProductDto.ProductName,
                ProductID = updateProductDto.ProductID,
                Price = updateProductDto.Price,
                ImageUrl = updateProductDto.ImageUrl,
                Description = updateProductDto.Description,
                CategoryID = updateProductDto.CategoryID
            });
            return Ok("Ürün Güncellendi");
        }
    }
}
