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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_orderService.GetAllOrders());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                return Ok(_orderService.GetOrderById(id));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("byCustomer/{customerId}/{sortOrderMethod=0}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetOrdersByCustomer(int customerId, int sortOrderMethod)
        {
            try
            {
                return Ok(_orderService.GetOrdersByCustomer(customerId, sortOrderMethod));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("byLocation/{locationId}/{sortOrderMethod=0}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetOrdersByLocation(int locationId, int sortOrderMethod)
        {
            try
            {
                return Ok(_orderService.GetOrdersByLocation(locationId, sortOrderMethod));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("items/{orderId}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetAllItemsInOrder(int orderId)
        {
            try
            {
                return Ok(_orderService.GetAllItemsInOrder(orderId));
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
        public IActionResult AddOrder(OrderInfo orderInfo)
        {
            Order order = new Order();
            order.CustomerId = orderInfo.CustomerId;
            order.LocationId = orderInfo.LocationId;
            order.OrderDate = DateTime.Now;
            
            try
            {
                _productService.UpdateProductStocksBatch(orderInfo.LocationId, orderInfo.Cart);
                _orderService.AddOrder(order, orderInfo.Cart, orderInfo.Prices);
                return CreatedAtAction("AddOrder", order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
