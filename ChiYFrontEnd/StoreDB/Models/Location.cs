using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Models
{
    public class Location
    {
        [Required]
        [ScaffoldColumn(false)]
        public int LocationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public List<Order> OrderHistory { get; set; }

        public List<ProductStock> ProductStocks { get; set; }
    }
}