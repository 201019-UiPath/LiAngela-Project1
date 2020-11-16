using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using StoreLib;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetAllLocations()
        {
            try
            {
                return Ok(_locationService.GetAllLocations());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [EnableCors("myAllowedOrigin")]
        public IActionResult GetLocationById(int id)
        {
            try
            {
                return Ok(_locationService.GetLocationById(id));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
