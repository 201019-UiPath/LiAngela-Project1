using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StoreDB;
using StoreDB.Repos;
using StoreWeb.Models;
using db = StoreDB.Models;

namespace StoreWeb.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationRepo _repo;

        public LocationController(ILocationRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllLocations()
        {
            string url = "https://localhost:44311/location";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.Location>>();
                    readTask.Wait();
                    var locations = readTask.Result;
                    return View(locations);
                }
                return View();
            }
        }

        /// <summary>
        /// Get location by locationId
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public IActionResult GetLocationById(int locationId)
        {
            string url = "https://localhost:44311/location/" + locationId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<db.Location>();
                    readTask.Wait();
                    var location = readTask.Result;
                    return View(location);
                }
                return View();
            }
        }
    }
}
