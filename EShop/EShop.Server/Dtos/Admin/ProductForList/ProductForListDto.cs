using EShop.Server.Interface;
using EShop.Server.Models;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin.ProductForList
{
    public class ProductForListDto:IAuditAble,IActiveAble
    {

        public int Id { get; set; }


        public ProductCatalogDto Catalog { get; set; }


        public string Url { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }


        public int Weight { get; set; }



        public decimal OriginalPrice { get; set; }

        public bool Deliver { get; set; }

        public bool ApplyPromotion { get; set; }

        public IEnumerable<ProductVersionDto> ProductVersions { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

     
        public string ModifiedBy { get; set; }
        public bool IsActive { get ; set; }
    }

    public class ProductVersionAttributeDto
    {
        public string AttributeName { get; set; }
        public int AtributeID { get; set; }
        public String AttributeValueName { get; set; }
        public int AttributeValueID { get; set; }



    }
    public class ProductVersionDto
    {
        public int Id { set; get; }

        public int WareHouseID { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int Quantity { set; get; }

        public string SKU { set; get; }

        public string Barcode { set; get; }


        public string ImageUrl { set; get; }

        public IEnumerable<ProductVersionAttributeDto> ProductVersionAttributes { set; get; }

        public IEnumerable<ProductVersionImageDto> ProductVersionImages { set; get; }
    }
    public class ProductVersionImageDto
    {

        public String Url { set; get; }

        public bool IsMain { set; get; }

    }

    public class AttributeValueDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class ProductCatalogDto
    {
        public int Id { set; get; }

        public string Name { set; get; }


    }
}
