using System;
using System.Collections.Generic;

using StoreDB.Models;
using StoreDB.Repos;

namespace StoreLib
{
    public class OrderService : IOrderService
    {
        private IOrderRepo repo;

        public OrderService(IOrderRepo repo)
        {
            this.repo = repo;
        }

        public List<Order> GetAllOrders()
        {
            return repo.GetAllOrders();
        }

        public int GetNewOrderId()
        {
            return repo.GetLastOrderId() + 1;
        }

        public int GetNewOrderItemId()
        {
            return repo.GetLastOrderItemId() + 1;
        }

        public Order GetOrderById(int orderId)
        {
            return repo.GetOrderById(orderId);
        }

        public List<Order> GetOrdersByCustomer(int customerId, int sortOrderMethod)
        {
            return repo.GetOrdersByCustomer(customerId, sortOrderMethod);
        }

        public List<Order> GetOrdersByLocation(int locationId, int sortOrderMethod)
        {
            return repo.GetOrdersByLocation(locationId, sortOrderMethod);
        }

        public List<OrderItem> GetAllItemsInOrder(int orderId)
        {
            return repo.GetAllItemsInOrder(orderId);
        }

        public void AddOrder(Order order, Dictionary<int, int> cart, Dictionary<int, decimal> prices)
        {
            int orderId = GetNewOrderId();
            order.OrderId = orderId;
            int orderItemId = GetNewOrderItemId();
            decimal subtotal = 0;
            foreach (KeyValuePair<int, int> item in cart)
            {
                if (item.Value > 0)
                {
                    AddOrderItem(orderItemId++, orderId, item.Key, item.Value);
                    subtotal += prices[item.Key] * item.Value;
                }
            }
            order.TotalPrice = subtotal + Math.Round(decimal.Multiply(subtotal, Convert.ToDecimal(0.0825)), 2);
            repo.AddOrder(order);
            repo.SaveOrderChanges();
        }

        public void AddOrderItem(int orderItemId, int orderId, int productId, int quantityOrdered)
        {
            OrderItem newOrderItem = new OrderItem();
            newOrderItem.Id = orderItemId;
            newOrderItem.OrderId = orderId;
            newOrderItem.ProductId = productId;
            newOrderItem.QuantityOrdered = quantityOrdered;
            repo.AddOrderItem(newOrderItem);
        }
    }
}