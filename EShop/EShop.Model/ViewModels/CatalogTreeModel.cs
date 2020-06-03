using EShop.EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.ViewModels
{
  public  class CatalogTreeModel
    {
        public Catalog Parent { set; get; }
        public IEnumerable<Catalog> Childs { set; get; }
    }
}
