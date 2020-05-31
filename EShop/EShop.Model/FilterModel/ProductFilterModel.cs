using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.FilterModel
{
    public class ProductFilterModel
    {
        public int? ID { set; get; }
        public String Name { set; get; }

        public int? CatalogID { set; get; }

        public int? NumVersion { set; get; }

        public int? FromWeight { set; get; }
        public int? ToWeight { set; get; }

        public bool SearchByMultiKeyword { set; get; }
        
    }
}
