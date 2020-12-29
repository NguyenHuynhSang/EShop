using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class ProductVersionRelatedDto
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public string MainImage { set; get; }
        public ProductRelatedDto Product { set; get; }
    }


    public class ProductRelatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
