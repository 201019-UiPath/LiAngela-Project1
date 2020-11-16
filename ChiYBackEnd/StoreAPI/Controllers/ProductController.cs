using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using StoreLib;
using StoreDB.Models;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetAllProducts()
        {
            try
            {
                return Ok(_productService.GetAllProducts());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                return Ok(_productService.GetProductById(id));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("byLocation/{locationId}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetProductStockByLocation(int locationId)
        {
            try
            {
                return Ok(_productService.GetProductStockByLocation(locationId));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("byProductId/{productId}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetProductStockByProductId(int productId)
        {
            try
            {
                return Ok(_productService.GetProductStockByProductId(productId));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("stock")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult UpdateProductStock(ProductStock productStock)
        {
            try
            {
                _productService.UpdateProductStockSingle(productStock.LocationId, productStock.ProductId, productStock.QuantityStocked);
                return StatusCode(200);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult AddProduct(Product newProduct)
        {
            try
            {
                _productService.AddProduct(newProduct);
                return CreatedAtAction("AddProduct", newProduct);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
