using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin.ProductForList
{
    public class ProductForListDto
    {

        public int ID { get; set; }


        public ProductCatalogDto Catalog { get; set; }


        public string Url { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }


        public int Weight { get; set; }



        public decimal OriginalPrice { get; set; }

        public bool Deliver { get; set; }

        public bool ApplyPromotion { get; set; }

        public IEnumerable<ProductVersionDto> ProductVersions { get; set; }



      

    }

    
}
