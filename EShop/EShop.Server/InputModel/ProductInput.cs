using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.InputModel
{


    public class ProductVersionInput
    {
        public String SKU { set; get; }
        public String Barcode { set; get; }

        public String Descripton { set; get; }
        public List<ProductAttributeInput> Attribute { set; get; }
    }
    public class ProductAttributeInput
    {
        public int AttributeValueID { set; get; }
        public int SelectedAttributeID { set; get; }


    }

    public class ProductInput
    {
        public String Name { get; set; }
        public String Content { set; get; }
        public IEnumerable<ProductVersionInput> Version { set; get; }
    }

}
