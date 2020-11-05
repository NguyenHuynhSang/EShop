using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin
{
    public class ProductForCreateDto
    {
        public int ID { set; get; }


        public int CatalogID { set; get; }

        public string Url { set; get; }


        public string Name { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }



        public decimal? OriginalPrice { get; set; }

        public bool Deliver { set; get; }

        public bool ApplyPromotion { set; get; }

        public IEnumerable<ProductVesionForCreateDto> ProductVersions { set; get; }

  
    }
    public class ProductVesionForCreateDto
    {

        public string Description { set; get; }

        public decimal Price { set; get; }

        public decimal PromotionPrice { set; get; }


        public int Quantum { set; get; }

        public int RemainingAmount { set; get; }

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
