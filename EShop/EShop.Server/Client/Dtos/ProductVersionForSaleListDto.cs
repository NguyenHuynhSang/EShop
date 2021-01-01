using EShop.Server.Dtos.Admin.ProductForList;
using EShop.Server.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos
{
    public class ProductVersionForSaleListDto
    {
        public string Name { set; get; }
        public ProductForSaleListDto Product { set; get; }
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int Quantity { set; get; }
        public int TotalSold { set; get; }
        public List<string> Tags { set; get; } = new List<string>() { "Thời trang", "Màu sắc", "Thuộc tính" };
        public string MainImage { set; get; }

        public IEnumerable<RelativeProductVersionDto> RelativeProductVersions { set; get; }

        public IEnumerable<ProductVersionImageDto> ProductVersionImages { set; get; }
    }

    public class ProductForSaleListDto
    {
        public int Id { get; set; }


        public int CatalogId { set; get; }

        public string CatalogName { set; get; }

        public int AverageRating { set; get; }
        public string Name { get; set; }
        public string Summary { get; set; }


        public decimal OriginalPrice { get; set; }
        public string SEOTitle { get; set; }
        public string SEOUrl { get; set; }
        public string SEODescription { get; set; }
        public DateTime? CreatedDate { get; set ; }
    }

}
