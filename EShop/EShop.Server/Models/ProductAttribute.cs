using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("ProductAttribute")] // map class với table trong csdl
    public class ProductAttribute
    {
        [Key] // chỉ định    khóa chính
        public int ID { set; get; }

        public int AttributeValueID { set; get; }

        public int ProductVersionID { set; get; }




    }
}
