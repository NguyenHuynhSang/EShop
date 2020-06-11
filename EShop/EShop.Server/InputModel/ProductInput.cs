using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.InputModel
{


    public class ProductVersionInput
    {
   
        public int ProductID { set; get; }

        public int WareHouseID { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }

        public int Quantum { set; get; }

        public int RemainingAmount { set; get; }

        public string SKU { set; get; }
        public string Barcode { set; get; }
        public List<ProductAttributeValue> Attributes { set; get; }
        public List<ProductVersionImage> Images { set; get; }
    }
  

    public class ProductInput
    {
  
        public long CatalogID { set; get; }
        public string Url { set; get; }
        public string Name { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public int Weight { set; get; }

    
        public decimal? OriginalPrice { get; set; }

        public bool Deliver { set; get; }

        public string SEOTitle { set; get; }

        public string SEOUrl { set; get; }

        public string SEODescription { set; get; }

        public bool ApplyPromotion { set; get; }
        public IEnumerable<ProductVersionInput> Versions { set; get; }
    }

}
