﻿using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin.ProductForList
{
    public class ProductVersionDto
    {
        public int ID { set; get; }

        public int WareHouseID { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }
        public decimal PromotionPrice { set; get; }
        public int Quantum { set; get; }

        public int RemainingAmount { set; get; }


        public string SKU { set; get; }

        public string Barcode { set; get; }


        public string ImageUrl { set; get; }

        public IEnumerable<ProductVersionAttributeDto> ProductVersionAttributes { set; get; }

        public IEnumerable<ProductVersionImageDto> ProductVersionImages { set; get; }
    }
}
