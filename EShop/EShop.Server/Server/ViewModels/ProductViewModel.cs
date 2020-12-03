using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.ViewModels
{


    public class ProductViewModel
    {
        public Product Product { set; get; }
        public ProductCatalog Catalog { set; get; }
        public IEnumerable<ProductVersionViewModel> ProductVersions { set; get; }
    }
}
