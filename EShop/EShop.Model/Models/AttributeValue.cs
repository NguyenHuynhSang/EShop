using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Models
{
    [Table("AttributeValue")]
    public class AttributeValue
    {
       
        [Key] // chỉ định    khóa chính
        public int ID { set; get; }

        [Required]
        public int AttributeID { set; get; }

        [StringLength(500)]
        public string Name { set; get; }

    }
}
