using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

using StoreDB.Repos;
using db = StoreDB.Models;
using StoreWeb.Models;

namespace StoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepo _repo;
        private db.Customer customer = new db.Customer();

        public HomeController(ILogger<HomeController> logger, ICustomerRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        /// Return Index view and verify user signin
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_UserId") != null)
            {
                return RedirectToAction("Home");
            }
            return View();
        }

        /// <summary>
        /// Return Home view
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            ViewData["CustomerId"] = HttpContext.Session.GetInt32("_UserId");
            ViewData["CustomerName"] = HttpContext.Session.GetString("_UserName");
            return View();
        }

        /// <summary>
        /// Sign in form view
        /// </summary>
        /// <returns></returns>
        public ViewResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// Sign in user by email address
        /// </summary>
        /// <param name="signingInCustomer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignIn(Customer signingInCustomer)
        {
            string url = "https://localhost:44311/customer/" + signingInCustomer.EmailAddress;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<db.Customer>();
                    readTask.Wait();
                    var customer = readTask.Result;
                    if (customer.Password.Equals(signingInCustomer.Password))
                    {
                        HttpContext.Session.SetInt32("_UserId", customer.CustomerId);
                        HttpContext.Session.SetString("_UserName", customer.Name);
                        Log.Information("User signed in");
                        return RedirectToAction("Home");
                    }
                    return View();
                }
                return View();
            }
        }

        /// <summary>
        /// Return About view
        /// </summary>
        /// <returns></returns>
        public ViewResult About()
        {
            return View();
        }

        /// <summary>
        /// Return History view
        /// </summary>
        /// <returns></returns>
        public ViewResult History()
        {
            return View();
        }

        /// <summary>
        /// Return Privacy view
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Sign out user using HttpContext.Session
        /// </summary>
        /// <returns></returns>
        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("_UserId");
            HttpContext.Session.Remove("_UserName");
            Log.Information("User signed out");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
