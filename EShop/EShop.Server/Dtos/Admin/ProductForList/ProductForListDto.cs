using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin.Product
{
    public class ProductForListDto
    {

        public int ID;


        public ProductCatalogDto Catalog;


        public string Url;

        public string Name;

        public string Description;

        public string Content;


        public int Weight;



        public decimal? OriginalPrice { get; set; }

        public bool Deliver;

        public bool ApplyPromotion;

        public IEnumerable<ProductVersionDto> ProductVersions;



      

    }

    
}
