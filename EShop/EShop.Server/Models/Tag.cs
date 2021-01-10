using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("Tag")]
    public class Tag
    {
        [Key] // chỉ định    khóa chính

        public int Id { set; get; } 
        public string Name { set; get; }
        public string EnName { set; get; }
    }
}
