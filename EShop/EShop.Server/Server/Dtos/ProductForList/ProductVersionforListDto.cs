using EShop.Server.Dtos.Admin.ProductForList;
using EShop.Server.Interface;
using EShop.Server.Models;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Dtos.ProductForList
{
    public class ProductVersionForListDto
    {
        public string Name { set; get; }
        
        public ProductForListVerDto Product { set; get; }
        public int Id { set; get; }

        public int WareHouseID { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int Quantity { set; get; }

        public string SKU { set; get; }

        public string Barcode { set; get; }


        public string MainImage { set; get; }

        public IEnumerable<ProductVersionAttributeDto> ProductVersionAttributes { set; get; }

        public IEnumerable<ProductVersionImageDto> ProductVersionImages { set; get; }
    }


    public class ProductForListVerDto : IAuditAble, IActiveAble
    {

        public int Id { get; set; }


        public int CatalogId { set; get; }

        public string CatalogName { set; get; }


        public string Url { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }


        public int Weight { get; set; }



        public decimal OriginalPrice { get; set; }

        public bool Deliver { get; set; }

        public bool ApplyPromotion { get; set; }


        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }


        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }

}
