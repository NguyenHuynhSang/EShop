using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos
{
    public class ProductCatalogForMenuDto
    {
        public int Id { set; get; }

       // public int? ParentID { set; get; }

        public string Name { set; get; }

        public string Image { set; get; }
        //public string SEOTitle { get; set; }
        //public string SEOUrl { get; set; }
        //public string SEODescription { get; set; }
        public List<ProductCatalogForMenuDto> ChildCatalogs { set; get; }


    }
}
