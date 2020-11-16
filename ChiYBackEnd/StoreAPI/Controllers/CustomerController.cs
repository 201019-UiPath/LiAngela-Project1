using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using StoreLib;
using StoreDB.Models;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                return Ok(_customerService.GetAllCustomers());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{emailAddress}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetCustomerByEmailAddress(string emailAddress)
        {
            try
            {
                return Ok(_customerService.GetCustomerByEmailAddress(emailAddress));
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
        public IActionResult AddCustomer(Customer newCustomer)
        {
            try
            {
                _customerService.AddCustomer(newCustomer);
                return CreatedAtAction("AddCustomer", newCustomer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
