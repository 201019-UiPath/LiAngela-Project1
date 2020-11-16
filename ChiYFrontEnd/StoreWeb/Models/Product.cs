using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWeb.Models
{
    public class Product
    {
        [Required]
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Trip Type")]
        public string TripType { get; set; }

        [Required]
        [DisplayName("Ticket Type")]
        public string TicketType { get; set; }

        [Required]
        [DisplayName("Passenger Type")]
        public string PassengerType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
    }
}
