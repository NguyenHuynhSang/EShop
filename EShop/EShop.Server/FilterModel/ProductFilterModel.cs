using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.FilterModel
{
    public class ProductFilterModel
    {
        public int? ID { set; get; }
        public String Name { set; get; }

        public int? CatalogID { set; get; }

        public int? FromNumVersion { set; get; }

        public int? ToNumVersion { set; get; }


        public int? FromWeight { set; get; }
        public int? ToWeight { set; get; }

        public int? FromPrice { set; get; }
        public int? ToPrice { set; get; }

        public int? FromOriginalPrice { set; get; }
        public int? ToOriginaPrice { set; get; }

        public bool SearchByMultiKeyword { set; get; }
        
    }
}
