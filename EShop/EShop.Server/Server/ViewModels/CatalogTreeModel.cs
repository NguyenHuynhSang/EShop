using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.ViewModels
{
  public  class CatalogTreeModel
    {
        public ProductCatalog Parent { set; get; }
        public IEnumerable<ProductCatalog> Childs { set; get; }
    }
}
