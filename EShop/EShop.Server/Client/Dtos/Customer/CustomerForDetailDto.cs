﻿using EShop.Server.Client.Dtos.Shipping;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class CustomerForDetailDto
    {
        public int Id { set; get; }

        public string UserName { set; get; }
        public string Name { set; get; }

        public string Phone { set; get; }


        public IEnumerable<AddressForViewDto>  Addresses {set;get;}

        public string Email { set; get; }
    }
}
