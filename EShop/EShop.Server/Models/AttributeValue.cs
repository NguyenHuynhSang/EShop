﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("AttributeValue")]
    public class AttributeValue
    {
       
        [Key] // chỉ định    khóa chính
        public int Id { set; get; }

        [Required]
        public int AttributeID { set; get; }


        [ForeignKey("AttributeID")]
        public Attribute Attribute { set; get; }

        [StringLength(500)]
        public string Name { set; get; }


        public IEnumerable<ProductVersionAttribute> ProductVersionAttributes { set; get; }
    }
}
