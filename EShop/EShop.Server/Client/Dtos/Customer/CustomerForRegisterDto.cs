using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class CustomerForRegisterDto
    {
        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "You must specify username between 4 and 64 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "You must specify password between 6 and 8 characters")]
        public string Password { get; set; }
    }
}
