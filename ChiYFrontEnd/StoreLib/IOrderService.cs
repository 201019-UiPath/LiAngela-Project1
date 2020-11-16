using StoreDB.Models;
using System.Collections.Generic;

namespace StoreLib
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();

        int GetNewOrderId();

        int GetNewOrderItemId();

        Order GetOrderById(int orderId);

        List<Order> GetOrdersByCustomer(int customerId, int sortOrderMethod);
        
        List<Order> GetOrdersByLocation(int locationId, int sortOrderMethod);

        List<OrderItem> GetAllItemsInOrder(int orderId);

        void AddOrder(Order order, Dictionary<int, int> cart, Dictionary<int, decimal> prices);

        void AddOrderItem(int orderItemId, int orderId, int productId, int quantityOrdered);
    }
}