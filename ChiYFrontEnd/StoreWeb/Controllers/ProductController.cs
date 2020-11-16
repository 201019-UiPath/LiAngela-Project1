using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Serilog;

using StoreDB;
using StoreDB.Repos;
using StoreWeb.Models;
using db = StoreDB.Models;

namespace StoreWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _repo;
        private db.Product product = new db.Product();

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all products
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
        /// Get product by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult GetProductById(int productId)
        {
            string url = "https://localhost:44311/product/" + productId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<db.Product>();
                    readTask.Wait();
                    var product = readTask.Result;
                    return View(product);
                }
                return View();
            }
        }

        /// <summary>
        /// Get product stock of a certain location by locationId
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
        /// Get product stock of a certain product by productId
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

        /// <summary>
        /// Form to update quantity of a certain product stock by locationId and productId
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ViewResult UpdateProductStock(int locationId, int productId)
        {
            ViewData["locationId"] = locationId;
            ViewData["productId"] = productId;
            return View();
        }

        /// <summary>
        /// Update product stock of a certain product at a certain location based on quantity in form input
        /// </summary>
        /// <param name="productStock"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateProductStock(ProductStock productStock)
        {
            string url = "https://localhost:44311/product/stock";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var postTask = client.PostAsJsonAsync("", productStock);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Log.Information("Product stock updated");
                    return RedirectToAction("GetAllProducts", "Manager");
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                    return View();
                }
            }
        }

        /// <summary>
        /// Form to add new product
        /// </summary>
        /// <returns></returns>
        public ViewResult AddProduct()
        {
            return View();
        }

        /// <summary>
        /// Add new product to product catalog
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                product.TripType = newProduct.TripType;
                product.TicketType = newProduct.TicketType;
                product.PassengerType = newProduct.PassengerType;
                product.Price = newProduct.Price;

                string url = "https://localhost:44311/product";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PostAsJsonAsync("", product);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<db.Product>();
                        readTask.Wait();
                        Log.Information("New product added");
                        return RedirectToAction("GetAllProducts", "Manager");
                    }
                    else
                    {
                        Console.WriteLine(result.StatusCode);
                        return View();
                    }
                }
            }
            else
                return View();
        }
    }
}
