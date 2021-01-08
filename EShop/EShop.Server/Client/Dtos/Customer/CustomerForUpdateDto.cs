using EShop.Server.Client.Dtos.Shipping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class CustomerForUpdateDto
    {
  
        public int Id { set; get; }
        [Required]
        public string Username { set; get; }
        [Required]
        public string Password { set; get; }
        [Required]
        public string Name { set; get; }


        public IEnumerable<AddressForUpdate> Addresses { set; get; }


        [Required]
        public string Phone { set; get; }

        public string AddressDetail { set; get; }

        public string Email { set; get; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
