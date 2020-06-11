﻿using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.ViewModels
{


    public class ProductViewModel
    {
        public Product Product { set; get; }
        public Catalog Catalog { set; get; }
        public IEnumerable<ProductVersion> ProductVersion{ set; get; }
    }
}
