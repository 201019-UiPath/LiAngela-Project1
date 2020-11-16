using System;
using System.Collections.Generic;

namespace StoreDB.Models
{
    public class OrderInfo
    {
        public int CustomerId { get; set; }

        public int LocationId { get; set; }

        public decimal Subtotal { get; set; }

        public List<ProductStock> AvailableOrderItems { get; set; }

        public Dictionary<int, int> Cart { get; set; }

        public Dictionary<int, decimal> Prices { get; set; }
    }
}