using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class ProductVersionAttribute
    {
        public int AttributeValueID { get; set; }
        public AttributeValue AttributeValue { get; set; }

        public int ProductVersionID { get; set; }
        public ProductVersion ProductVersion { get; set; }

    }
}
