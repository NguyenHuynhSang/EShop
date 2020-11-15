using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin
{
    public class ProductForInputDto
    {
        public int Id { get; set; }

        public int CatalogID { set; get; }

        public string Url { set; get; }


        public string Name { set; get; }

        public string Summary { set; get; }

        public string Description { set; get; }



        public decimal OriginalPrice { get; set; }

        public bool Deliver { set; get; }

        public bool ApplyPromotion { set; get; }

        public IEnumerable<ProductVesionForCreateDto> ProductVersions { set; get; }

  
    }
    public class ProductVesionForCreateDto
    {

        public string Description { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }


        public int Quantity { set; get; }

        public string SKU { set; get; }
        public string Barcode { set; get; }

       

        public IEnumerable<ProductVersionAttributeForCreateDto> ProductVersionAttributes { set; get; }

        public IEnumerable<ProductVersionImagesForCreateDto> ProductVersionImages { set; get; }


    }

    public class ProductVersionAttributeForCreateDto
    {
        public int AttributeValueID { set; get; }
    }
    public class ProductVersionImagesForCreateDto
    {
        public String Url { set; get; }

        public bool IsMain { set; get; }
    }



}
