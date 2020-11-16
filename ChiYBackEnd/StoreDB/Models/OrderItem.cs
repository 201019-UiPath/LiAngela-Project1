using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Models
{
    public class OrderItem
    {
        [Required]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        public Order Order { get; set; }
        
        public Product Product { get; set; }

        [Required]
        [DisplayName("Quantity Ordered")]
        public int QuantityOrdered { get; set; }
    }
}