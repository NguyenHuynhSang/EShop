using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.ViewModels
{
   public class CatalogViewModel
    {
        public String ParentName { set; get; }
        public ProductCatalog Catalog { set; get; }
    }
}
