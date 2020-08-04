using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class ProductVersionAttribute
    {
        public int AttributeValueID { get; set; }

        [ForeignKey("AttributeValueID")]
        public AttributeValue AttributeValue { get; set; }

        public int ProductVersionID { get; set; }

        [ForeignKey("ProductVersionID")]
        public ProductVersion ProductVersion { get; set; }

    }
}
