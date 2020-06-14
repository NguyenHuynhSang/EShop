using EShop.Server.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("Catalog")]
    public class ProductCatalog : SeoAndAudit
    {


        [Key] // chỉ định    khóa chính
        public int ID { set; get; }

        public int? ParentID { set; get; }

        [StringLength(500)]
        public string Name { set; get; }

      


    }
}
