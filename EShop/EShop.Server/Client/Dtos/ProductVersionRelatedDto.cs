using EShop.Server.Dtos.Admin.ProductForList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Customer
{
    public class ProductVersionRelatedDto
    {
        public int Id { set; get; }
        public ProductRelatedDto Product { set; get; }
        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int Quantity { set; get; }
        public string MainImage { set; get; }
        public string SKU { set; get; }
        public IEnumerable<ProductVersionImageDto> ProductVersionImages { set; get; }
      
        public IEnumerable<RelativeProductVersionDto> RelativeProductVersions { set; get; }
    }


    public class ProductRelatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int CatalogId { set; get; }
        public int? ParentCatalogId { set; get; }
        public string CatalogName { set; get; }
        public int AverageRating { set; get; }
        public int NumOfComments { set; get; }

    }
}
