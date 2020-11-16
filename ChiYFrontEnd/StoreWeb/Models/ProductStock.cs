using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWeb.Models
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

        [Required]
        [DisplayName("Quantity to Stock")]
        public int QuantityStocked { get; set; }
    }
}
