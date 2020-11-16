using System.Collections.Generic;

using StoreDB.Models;

namespace StoreDB.Repos
{
    public interface IOrderRepo
    {
         List<Order> GetAllOrders();

         int GetLastOrderId();

         int GetLastOrderItemId();

         Order GetOrderById(int orderId);

         List<Order> GetOrdersByCustomer(int customerId, int sortOrderMethod);

         List<Order> GetOrdersByLocation(int locationId, int sortOrderMethod);

         List<OrderItem> GetAllItemsInOrder(int orderId);

        void AddOrder(Order order);

        void AddOrderItem(OrderItem orderItem);

        void SaveOrderChanges();
    }
}