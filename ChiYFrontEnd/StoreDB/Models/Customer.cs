using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Models
{
    public class Customer
    {
        [Required]
        [ScaffoldColumn(false)]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"^\w+@\w+\.\w{3}$", ErrorMessage = "Email address should follow the format username@email.com")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Order> OrderHistory { get; set; }
    }
}