using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class CustomerForOrderDto
    {
        public int Id { set; get; }

        public string UserName { set; get; }
        public string Name { set; get; }

        public string Phone { set; get; }

        public Address MainAddress { set; get; }


        public string Email { set; get; }
    }
}
