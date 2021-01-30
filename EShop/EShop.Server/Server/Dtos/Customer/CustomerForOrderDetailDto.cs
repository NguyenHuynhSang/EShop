using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Dtos.Customer
{
    public class CustomerForOrderDetailDto
    {
        public int Id { set; get; }

        public string UserName { set; get; }
        public string Name { set; get; }

        public string Phone { set; get; }


        public string Email { set; get; }
    }
}
