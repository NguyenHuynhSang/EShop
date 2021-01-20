using EShop.Server.Interface;
using EShop.Server.Models;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Server.Dtos.Admin
{
    public class ProductForInputDto:ISeoAble,IActiveAble
    {
     
        [DefaultValue(6)]
        public int CatalogID { set; get; }

        public string Url { set; get; }


        [DefaultValue("Sản phẩm mặc định")]
        public string Name { set; get; }

        public string Summary { set; get; }

        public string Description { set; get; }

        public decimal OriginalPrice { get; set; }

        public bool Deliver { set; get; }

        public bool ApplyPromotion { set; get; }

        public IEnumerable<ProductVesionForCreateDto> ProductVersions { set; get; }


        public int Weight { get; set; }
        public string SEOTitle { get; set; }
        public string SEOUrl { get ; set ; }
        public string SEODescription { get ; set ; }
        public bool IsActive { get ; set; }
   
    }
    public class ProductVesionForCreateDto
    {

        public string Name { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }


        public int Quantity { set; get; }

        public string SKU { set; get; }
        public string Barcode { set; get; }

       

        public IEnumerable<ProductVersionAttributeForCreateDto> ProductVersionAttributes { set; get; }
        public IEnumerable<ProductVersionTagsForCreateDto> ProductVersionTags { set; get; }

        public IEnumerable<ProductVersionImagesForCreateDto> ProductVersionImages { set; get; }
        
   
    }

    public class ProductVersionAttributeForCreateDto
    {
        public int AttributeValueID { set; get; }
    }


    public class ProductVersionTagsForCreateDto
    {
        public int TagId { get; set; }
    }

    public class ProductVersionImagesForCreateDto
    {
        public String Url { set; get; }

        public bool IsMain { set; get; }

        public string PublicId { get; set; }

    }



}
