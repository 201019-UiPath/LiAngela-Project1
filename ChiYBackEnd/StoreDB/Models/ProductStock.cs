using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Models
{
    public class ProductStock
    {
        [Required]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int LocationId { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        public Location Location { get; set; }
        
        public Product Product { get; set; }

        [Required]
        [DisplayName("Quantity in Stock")]
        public int QuantityStocked { get; set; }
    }
}