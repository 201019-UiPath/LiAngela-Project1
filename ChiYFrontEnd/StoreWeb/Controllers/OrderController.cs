using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Serilog;

using StoreDB;
using StoreDB.Repos;
using StoreWeb.Models;
using db = StoreDB.Models;

namespace StoreWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo _repo;
        private db.Order order = new db.Order();
        private db.OrderInfo orderInfo = new db.OrderInfo();

        public OrderController(IOrderRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllOrders()
        {
            string url = "https://localhost:44311/order";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.Order>>();
                    readTask.Wait();
                    var orders = readTask.Result;
                    return View(orders);
                }
                return View();
            }
        }

        /// <summary>
        /// Get orders by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult GetOrderById(int orderId)
        {
            string url = "https://localhost:44311/order/" + orderId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<db.Order>();
                    readTask.Wait();
                    var order = readTask.Result;
                    return View(order);
                }
                return View();
            }
        }

        /// <summary>
        /// Get orders by customerId and sort orders by sortOrderMethod
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="sortOrderMethod"></param>
        /// <returns></returns>
        public IActionResult GetOrdersByCustomer(int customerId, int sortOrderMethod)
        {
            ViewData["customerId"] = customerId;
            ViewData["sortOrderMethod"] = sortOrderMethod;
            string url = $"https://localhost:44311/order/byCustomer/{customerId}/{sortOrderMethod}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.Order>>();
                    readTask.Wait();
                    var orders = readTask.Result;
                    return View(orders);
                }
                return View();
            }
        }

        /// <summary>
        /// Get orders by locationId and sort orders by sortOrderMethod
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="sortOrderMethod"></param>
        /// <returns></returns>
        public IActionResult GetOrdersByLocation(int locationId, int sortOrderMethod)
        {
            ViewData["locationId"] = locationId;
            ViewData["sortOrderMethod"] = sortOrderMethod;
            string url = $"https://localhost:44311/order/byLocation/{locationId}/{sortOrderMethod}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.Order>>();
                    readTask.Wait();
                    var orders = readTask.Result;
                    return View(orders);
                }
                return View();
            }
        }

        /// <summary>
        /// Get all OrderItems in a certain order by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult GetAllItemsInOrder(int orderId)
        {
            string url = "https://localhost:44311/order/items/" + orderId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.OrderItem>>();
                    readTask.Wait();
                    var orderItems = readTask.Result;
                    return View(orderItems);
                }
                return View();
            }
        }

        /// <summary>
        /// Form to select quantity and place order using the OrderInfo model
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public ViewResult AddOrderInfo(int locationId)
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
                    OrderInfo oi = new OrderInfo();
                    oi.AvailableOrderItems = productStocks;
                    oi.LocationId = locationId;
                    return View(oi);
                }
                return View();
            }
        }

        /// <summary>
        /// Update ProductStocks, add OrderItems and add Order for a certain order using OrderInfo model form data
        /// </summary>
        /// <param name="newOrderInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOrderInfo(OrderInfo newOrderInfo)
        {
            if (HttpContext.Session.GetInt32("_UserId") == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    orderInfo.CustomerId = (int)HttpContext.Session.GetInt32("_UserId");
                    orderInfo.LocationId = newOrderInfo.LocationId;
                    orderInfo.AvailableOrderItems = null;
                    orderInfo.Cart = newOrderInfo.Cart;
                    orderInfo.Prices = newOrderInfo.Prices;

                    string url = "https://localhost:44311/order";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        var postTask = client.PostAsJsonAsync("", orderInfo);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<db.Order>();
                            readTask.Wait();
                            Log.Information("Order placed");
                            return RedirectToAction("Index", "Home");
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
}
