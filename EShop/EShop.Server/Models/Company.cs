using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class Company
    {
        [Key]
        public long Id { get; set; }
        public string Name { set; get; }
        public string AddressDetail { set; get; }
        public string Email { set; get; }
        public string WardCode { set; get; }
        public string Phone { set; get; }
        public string OwerName { set; get; }

    }
}
