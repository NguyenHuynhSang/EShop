using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class ProductVersionTag
    {
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; private set; }

        public int ProductVersionID { get; private set; }

        [ForeignKey("ProductVersionID")]
        public ProductVersion ProductVersion { get; private set; }
    }
}
