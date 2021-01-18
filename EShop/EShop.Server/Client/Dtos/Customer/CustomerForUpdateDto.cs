using EShop.Server.Client.Dtos.Shipping;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class CustomerForUpdateDto
    {
        [Required]
        public int Id { set; get; }
        public string Password { set; get; }
        public string CurrentPass { set; get; }
        public string Name { set; get; }

        public string Phone { set; get; }

        public string AddressDetail { set; get; }

        public string Email { set; get; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class AddressForInputDto
    {
        public string WardCode { set; get; }
        public string AddressDetail { set; get; }
        public bool isMain { set; get; }
    }
}
