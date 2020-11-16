using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using StoreDB;
using StoreDB.Repos;
using StoreWeb.Models;
using db = StoreDB.Models;

namespace StoreWeb.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IProductRepo _repo;
        private db.Product product = new db.Product();

        public ManagerController(IProductRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Return Index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all products view with manager actions
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllProducts()
        {
            string url = "https://localhost:44311/product";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.Product>>();
                    readTask.Wait();
                    var products = readTask.Result;
                    return View(products);
                }
                return View();
            }
        }

        /// <summary>
        /// Get all locations view with manager actions
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
        /// Get product stock of a certain location by locationId with manager actions
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public IActionResult GetProductStockByLocation(int locationId)
        {
            string url = "https://localhost:44311/product/byLocation/" + locationId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.ProductStock>>();
                    readTask.Wait();
                    var productStocks = readTask.Result;
                    return View(productStocks);
                }
                return View();
            }
        }

        /// <summary>
        /// Get product stock of a certain product by productId with manager actions
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult GetProductStockByProductId(int productId)
        {
            string url = "https://localhost:44311/product/byProductId/" + productId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.ProductStock>>();
                    readTask.Wait();
                    var productStocks = readTask.Result;
                    return View(productStocks);
                }
                return View();
            }
        }
    }
}
