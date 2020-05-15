using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Models
{
    [Table("Attribute")]
    public class Attribute
    {
        
        [Key] // chỉ định    khóa chính
        public int ID { set; get; }

        [StringLength(500)]
        public string Name { set; get; }

    }
}
