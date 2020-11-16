using System;
using Xunit;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using StoreDB;
using StoreDB.Models;
using StoreDB.Repos;
using StoreLib;

namespace StoreTest
{
    public class StoreTest
    {
        private DBRepo repo;
        
        private void Seed(StoreContext testContext) {
            testContext.Customers.AddRange(testCustomers);
            testContext.Locations.AddRange(testLocations);
            testContext.Products.AddRange(testProducts);
            testContext.ProductStocks.AddRange(testProductStocks);
            testContext.SaveChanges();
        }

        private readonly Customer testCustomer = new Customer()
        {
            Name = "Test Customer",
            PhoneNumber = "(000) 000 0000",
            EmailAddress = "test@customer.com",
            Password = "test"
        };

        private readonly Order testOrder = new Order()
        {
            OrderId = 1,
            CustomerId = 1,
            LocationId = 1,
            OrderDate = DateTime.Now
        };

        private readonly List<Customer> testCustomers = new List<Customer>()
        {
            new Customer {
                Name = "Test1 Customer",
                PhoneNumber = "(000) 000 0000",
                EmailAddress = "test1@customer.com",
                Password = "test1"
            },
            new Customer {
                Name = "Testing Testing",
                PhoneNumber = "(000) 000 0000",
                EmailAddress = "test2@customer.com",
                Password = "test2"
            }
        };

        private readonly List<Location> testLocations = new List<Location>()
        {
            new Location {LocationId = 1, Name = "Test Location 1", PhoneNumber = "(111) 111 1111", Address = "111 Main St"},
            new Location {LocationId = 2, Name = "Test Location 2", PhoneNumber = "(211) 111 1111", Address = "211 Main St"}
        };

        private readonly List<Product> testProducts = new List<Product>()
        {
            new Product { ProductId = 1, TripType = "Test", TicketType = "One-Way", PassengerType = "Child", Price = 2 },
            new Product { ProductId = 2, TripType = "Test", TicketType = "Week Pass", PassengerType = "Child", Price = 4 },
            new Product { ProductId = 3, TripType = "Test", TicketType = "Month Pass", PassengerType = "Child", Price = 6 },
            new Product { ProductId = 4, TripType = "Test", TicketType = "One-Way", PassengerType = "Adult", Price = 8 },
            new Product { ProductId = 5, TripType = "Test", TicketType = "One-Way", PassengerType = "Senior", Price = 10 }
        };
        
        private readonly List<ProductStock> testProductStocks = new List<ProductStock>()
        {
            new ProductStock {Id = 1, LocationId = 1, ProductId = 1, QuantityStocked = 10},
            new ProductStock {Id = 2, LocationId = 1, ProductId = 2, QuantityStocked = 10},
            new ProductStock {Id = 3, LocationId = 2, ProductId = 3, QuantityStocked = 10},
            new ProductStock {Id = 4, LocationId = 2, ProductId = 4, QuantityStocked = 10},
            new ProductStock {Id = 5, LocationId = 2, ProductId = 5, QuantityStocked = 10}
        };

        [Fact]
        public void AddCustomerShouldAddCustomer()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("AddCustomerShouldAddCustomer").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);

            repo.AddCustomer(testCustomer);

            using var assertContext = new StoreContext(options);
            Assert.NotNull(assertContext.Customers.Single(c => c.Name == testCustomer.Name));
        }

        [Fact]
        public void GetAllCustomersShouldGetAllCustomers()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetAllCustomersShouldGetAllCustomers").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            List<Customer> customers = repo.GetAllCustomers();

            using var assertContext = new StoreContext(options);
            Assert.NotNull(customers);
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public void GetCustomerByEmailAddressShouldGetGustomerByEmailAddress()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetCustomerByEmailAddressShouldGetGustomerByEmailAddress").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            Customer customer = repo.GetCustomerByEmailAddress("test1@customer.com");

            using var assertContext = new StoreContext(options);
            Assert.NotNull(customer);
            Assert.Equal("Test1 Customer", customer.Name);
        }

        [Fact]
        public void GetAllLocationsShouldGetAllLocations()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetAllLocationsShouldGetAllLocations").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            List<Location> locations = repo.GetAllLocations();

            using var assertContext = new StoreContext(options);
            Assert.NotNull(locations);
            Assert.Equal(2, locations.Count);
        }

        [Fact]
        public void GetLocationByIdShouldGetLocationById()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetLocationByIdShouldGetLocationById").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            Location location = repo.GetLocationById(1);

            using var assertContext = new StoreContext(options);
            Assert.NotNull(location);
            Assert.Equal("Test Location 1", location.Name);
        }

        [Fact]
        public void GetAllProductsShouldGetAllProducts()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetAllProductsShouldGetAllProducts").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            List<Product> products = repo.GetAllProducts();

            using var assertContext = new StoreContext(options);
            Assert.NotNull(products);
            Assert.Equal(5, products.Count);
        }

        [Fact]
        public void GetProductByIdShouldGetProductById()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetProductByIdShouldGetProductById").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            Product product = repo.GetProductById(1);

            using var assertContext = new StoreContext(options);
            Assert.NotNull(product);
            Assert.Equal("One-Way", product.TicketType);
        }

        [Fact]
        public void GetProductStockByLocationShouldGetProductStockByLocation()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetProductStockByLocationShouldGetProductStockByLocation").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            List<ProductStock> productStocks = repo.GetProductStockByLocation(1);

            using var assertContext = new StoreContext(options);
            Assert.NotNull(productStocks);
            Assert.Equal(2, productStocks.Count);
        }

        [Fact]
        public void GetProductStockByProductIdShouldGetProductStockByProductId()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetProductStockByProductIdShouldGetProductStockByProductId").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            Seed(testContext);

            List<ProductStock> productStocks = repo.GetProductStockByProductId(1);

            using var assertContext = new StoreContext(options);
            Assert.NotNull(productStocks);
            Assert.Single(productStocks);
        }

        [Fact]
        public void AddOrderShouldAddOrder()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("AddOrderShouldAddOrder").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);

            repo.AddOrder(testOrder);

            using var assertContext = new StoreContext(options);
            Assert.NotNull(assertContext.Orders.Single(o => o.OrderId == testOrder.OrderId));
        }
    }
}
