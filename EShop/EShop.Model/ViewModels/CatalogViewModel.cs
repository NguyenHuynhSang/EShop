using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.ViewModels
{
   public class CatalogViewModel
    {
        public String ParentName { set; get; }
        public Catalog Catalog { set; get; }
    }
}
