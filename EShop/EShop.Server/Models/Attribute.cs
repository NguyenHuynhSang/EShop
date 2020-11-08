﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EShop.Server.Models
{
    [Table("Attribute")]
    public class Attribute
    {
        
        [Key] // chỉ định    khóa 
       

        public int Id { set; get; }

        [StringLength(500)]
        public string Name { set; get; }

    }
}
