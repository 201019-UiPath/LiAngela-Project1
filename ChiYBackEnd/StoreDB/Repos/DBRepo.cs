using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using StoreDB.Models;

namespace StoreDB.Repos
{
    public class DBRepo : ICustomerRepo, ILocationRepo, IOrderRepo, IProductRepo
    {
        private StoreContext context;

        public DBRepo(StoreContext context)
        {
            this.context = context;
        }

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            context.OrderItems.Add(orderItem);
        }

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void AddProductStock(ProductStock productStock)
        {
            context.ProductStocks.Add(productStock);
        }

        public List<Customer> GetAllCustomers()
        {
            return context.Customers.Select(x => x).ToList();
        }

        public List<OrderItem> GetAllItemsInOrder(int orderId)
        {
            return context.OrderItems.Where(x => x.OrderId == orderId)
            .Include("Product")
            .ToList();
        }

        public List<Location> GetAllLocations()
        {
            return context.Locations.Select(x => x).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return context.Orders.Select(x => x)
            .Include("Location")
            .OrderBy(x => x.OrderDate)
            .ToList();
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.Select(x => x).OrderBy(x => x.ProductId).ToList();
        }

        public Customer GetCustomerByEmailAddress(string customerEmailAddress)
        {
            return context.Customers.Where(x => x.EmailAddress == customerEmailAddress).SingleOrDefault();
        }

        public int GetLastCustomerId()
        {
            if (context.Customers.Select(x => x).Count() == 0)
            {
                return 0;
            }
            else
            {
                return context.Customers.Select(x => x.CustomerId).Max();
            }
        }

        public int GetLastOrderId()
        {
            if (context.Orders.Select(x => x).Count() == 0)
            {
                return 0;
            }
            else
            {
                return context.Orders.Select(x => x.OrderId).Max();
            }
        }

        public int GetLastOrderItemId()
        {
            if (context.OrderItems.Select(x => x).Count() == 0)
            {
                return 0;
            }
            else
            {
                return context.OrderItems.Select(x => x.Id).Max();
            }
        }

        public int GetLastProductId()
        {
            if (context.Products.Select(x => x).Count() == 0)
            {
                return 0;
            }
            else
            {
                return context.Products.Select(x => x.ProductId).Max();
            }
        }

        public int GetLastProductStockId()
        {
            if (context.ProductStocks.Select(x => x).Count() == 0)
            {
                return 0;
            }
            else
            {
                return context.ProductStocks.Select(x => x.Id).Max();
            }
        }

        public Location GetLocationById(int locationId)
        {
            return context.Locations.Where(x => x.LocationId == locationId).SingleOrDefault();
        }

        public Order GetOrderById(int orderId)
        {
            return context.Orders.Where(x => x.OrderId == orderId)
                .Include("Customer")
                .Include("Location")
                .SingleOrDefault();
        }

        public List<Order> GetOrdersByCustomer(int customerId, int sortOrderMethod)
        {
            if (sortOrderMethod == 1)
            {
                return context.Orders.Where(x => x.CustomerId == customerId)
                .Include("Location")
                .OrderByDescending(x => x.OrderDate)
                .ToList();
            }
            else if (sortOrderMethod == 2)
            {
                return context.Orders.Where(x => x.CustomerId == customerId)
                .Include("Location")
                .OrderBy(x => x.TotalPrice)
                .ToList();
            }
            else if (sortOrderMethod == 3)
            {
                return context.Orders.Where(x => x.CustomerId == customerId)
                .Include("Location")
                .OrderByDescending(x => x.TotalPrice)
                .ToList();
            }
            else
            {
                return context.Orders.Where(x => x.CustomerId == customerId)
                .Include("Location")
                .OrderBy(x => x.OrderDate)
                .ToList();
            }
        }

        public List<Order> GetOrdersByLocation(int locationId, int sortOrderMethod)
        {
            if (sortOrderMethod == 1)
            {
                return context.Orders.Where(x => x.LocationId == locationId)
                .Include("Customer")
                .OrderByDescending(x => x.OrderDate)
                .ToList();
            }
            else if (sortOrderMethod == 2)
            {
                return context.Orders.Where(x => x.LocationId == locationId)
                .Include("Customer")
                .OrderBy(x => x.TotalPrice)
                .ToList();
            }
            else if (sortOrderMethod == 3)
            {
                return context.Orders.Where(x => x.LocationId == locationId)
                .Include("Customer")
                .OrderByDescending(x => x.TotalPrice)
                .ToList();
            }
            else
            {
                return context.Orders.Where(x => x.LocationId == locationId)
                .Include("Customer")
                .OrderBy(x => x.OrderDate)
                .ToList();
            }
        }

        public Product GetProductById(int productId)
        {
            return context.Products.Where(x => x.ProductId == productId).SingleOrDefault();
        }

        public List<ProductStock> GetProductStockByLocation(int locationId)
        {
            return context.ProductStocks.Where(x => x.LocationId == locationId)
            .Include("Product")
            .Include("Location")
            .OrderBy(x => x.ProductId)
            .ToList();
        }

        public ProductStock GetProductStockByLocationProductId(int locationId, int productId)
        {
            return context.ProductStocks.Where(x => x.ProductId == productId && x.LocationId == locationId).SingleOrDefault();
        }

        public List<ProductStock> GetProductStockByProductId(int productId)
        {
            return context.ProductStocks.Where(x => x.ProductId == productId)
            .Include("Product")
            .Include("Location")
            .OrderBy(x => x.ProductId)
            .ToList();
        }

        public void RemoveProductStock(ProductStock productStock)
        {
            context.ProductStocks.Remove(productStock);
        }

        public void SaveOrderChanges()
        {
            context.SaveChanges();
        }

        public void SaveProductStockChanges()
        {
            context.SaveChanges();
        }

        public void UpdateProductStock(ProductStock productStock)
        {
            context.ProductStocks.Update(productStock);
        }
    }
}
