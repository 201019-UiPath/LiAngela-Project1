using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

using StoreDB;
using StoreDB.Repos;
using StoreWeb.Models;
using db = StoreDB.Models;

namespace StoreWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _repo;
        private db.Customer customer = new db.Customer();

        public CustomerController(ICustomerRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllCustomers()
        {
            string url = "https://localhost:44311/customer";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<db.Customer>>();
                    readTask.Wait();
                    var customers = readTask.Result;
                    return View(customers);
                }
                return View();
            }
        }

        /// <summary>
        /// Get customer by email address
        /// </summary>
        /// <param name="customerEmailAddress"></param>
        /// <returns></returns>
        public IActionResult GetCustomerByEmailAddress(string customerEmailAddress)
        {
            string url = "https://localhost:44311/customer/" + customerEmailAddress;
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
                    return View(customer);
                }
                return View();
            }
        }

        /// <summary>
        /// Sign up form view for customer signup
        /// </summary>
        /// <returns></returns>
        public ViewResult AddCustomer()
        {
            return View();
        }

        /// <summary>
        /// Sign up customer and add to DB
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddCustomer(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                customer.Name = newCustomer.Name;
                customer.PhoneNumber = newCustomer.PhoneNumber;
                customer.EmailAddress = newCustomer.EmailAddress;
                customer.Password = newCustomer.Password;

                string url = "https://localhost:44311/customer";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PostAsJsonAsync("", customer);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<db.Customer>();
                        readTask.Wait();
                        Log.Information("Customer signed up");
                        return RedirectToAction("SignIn", "Home");
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
